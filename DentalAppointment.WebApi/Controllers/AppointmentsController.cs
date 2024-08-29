using Asp.Versioning;
using DentalAppointment.Commands.Commands;
using DentalAppointment.Core.Queries;
using DentalAppointment.Queries.Queries;
using DentalAppointment.Query.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand appointmentCommand)
        {
            var result = await mediator.Send(appointmentCommand);

            return CreatedAtAction(nameof(CreateAppointment), new { Appointment = result.AppointmentDateTime }, result);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{appointmentDate}")]
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
        public async Task<IActionResult> GetAllAppointments()
        {
            var query = new GetAllAppointmentsQuery();

            var result = await mediator.Send(query);

            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPut]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommand updateAppointmentCommand)
        {
            await mediator.Send(updateAppointmentCommand);

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment([FromBody] DeleteAppointmentCommand deleteAppointmentDateTime)
        {
            await mediator.Send(deleteAppointmentDateTime);

            return NoContent();
        }
    }
}