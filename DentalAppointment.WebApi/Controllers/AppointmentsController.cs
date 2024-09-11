using Asp.Versioning;
using DentalAppointment.Commands.Commands;
using DentalAppointment.Core.Queries;
using DentalAppointment.Entities.Responses;
using DentalAppointment.Infrastructure.Templates;
using DentalAppointment.Queries.Queries;
using DentalAppointment.Query.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DentalAppointment.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AppointmentsController(IMediator mediator,
                                        IValidator<GetAppointmentByDateTimeQuery> dateTimeValidator,
                                        IValidator<GetAppointmentByIdQuery> idValidator) : ControllerBase
    {
        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AppointmentResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand appointmentCommand)
        {
            var result = await mediator.Send(appointmentCommand);

            return CreatedAtAction(nameof(CreateAppointment), new { Appointment = result.AppointmentDateTime }, result);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{appointmentDate}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AppointmentResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAppointmentByDateTime(DateTime appointmentDate)
        {
            var query = new GetAppointmentByDateTimeQuery(appointmentDate);

            var validationResult = dateTimeValidator.Validate(query);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var result = await mediator.Send(query);

            return result != null ? Ok(result) : NotFound("Appointment not found.");
        }

        [MapToApiVersion("1.0")]
        [HttpGet("id/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AppointmentResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var query = new GetAppointmentByIdQuery(id);

            var validationResult = idValidator.Validate(query);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var result = await mediator.Send(query);

            return result != null ? Ok(result) : NotFound("Appointment not found.");
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<AppointmentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllAppointmentsQuery();

            var result = await mediator.Send(query);

            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommand updateAppointmentCommand)
        {
            await mediator.Send(updateAppointmentCommand);

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAppointment([FromBody] DeleteAppointmentCommand deleteAppointmentDateTime)
        {
            await mediator.Send(deleteAppointmentDateTime);

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpGet("confirm/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConfirmAppointment(Guid id, [FromQuery] bool confirm)
        {
            var query = new GetAppointmentByIdQuery(id);

            var validationResult = idValidator.Validate(query);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var appointment = await mediator.Send(query);

            if (appointment == null)
                return NotFound("Appointment not found.");

            if (appointment.IsConfirmed && !appointment.IsRejected)
                return Content(EmailTemplates.GetAppointmentConfirmationTemplate(true, appointment.AppointmentDateTime), "text/html");
            else if (!appointment.IsConfirmed && appointment.IsRejected)
                return Content(EmailTemplates.GetAppointmentConfirmationTemplate(false, appointment.AppointmentDateTime), "text/html");

            var updateAppointmentCommand = new UpdateAppointmentCommand
            {
                AppointmentDateTime = appointment.AppointmentDateTime,
                NewAppointmentDateTime = null,
                PatientName = appointment.PatientName,
                PatientPhoneNumber = appointment.PatientPhoneNumber,
                TreatmentType = appointment.TreatmentType,
                Notes = appointment.Notes,
                IsConfirmed = confirm,
                IsRejected = !confirm
            };

            await mediator.Send(updateAppointmentCommand);

            return Content(EmailTemplates.GetAppointmentConfirmationTemplate(confirm, appointment.AppointmentDateTime), "text/html");
        }
    }
}