using Asp.Versioning;
using AutoMapper;
using DentalAppointment.Core.Dtos;
using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace DentalAppointment.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AppointmentsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                return BadRequest("No candidate data provided.");

            if (!TryValidateModel(appointmentDto))
                return BadRequest(ModelState);

            var existingAppoint = await unitOfWork.AppointmentRepository.GetByDateAsync(appointmentDto.AppointmentDate);

            if (existingAppoint != null)
                return Conflict("Sorry but an appointment already exist.");

            var entity = mapper.Map<AppointmentModel>(appointmentDto);

            await unitOfWork.AppointmentRepository.AddAsync(entity);

            return CreatedAtAction(nameof(CreateAppointment), new { Appointment = entity.AppointmentDate }, entity);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{appointmentDate}")]
        public async Task<IActionResult> GetAppointmentByDateTime(DateTime appointmentDate)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetByDateAsync(appointmentDate);

            if (appointment == null)
                return NotFound("Appointment not found.");

            var appointmentDto = mapper.Map<AppointmentDto>(appointment);

            return Ok(appointmentDto);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetAsync(id);

            if (appointment == null)
                return NotFound("Appointment not found.");

            var appointmentDto = mapper.Map<AppointmentDto>(appointment);

            return Ok(appointmentDto);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                return BadRequest("No candidate data provided.");

            if (!TryValidateModel(appointmentDto))
                return BadRequest(ModelState);

            var existingAppointment = await unitOfWork.AppointmentRepository.GetAsync(id);

            if (existingAppointment == null)
                return NotFound("Appointment not found.");

            mapper.Map(appointmentDto, existingAppointment);

            await unitOfWork.AppointmentRepository.UpdateAsync(existingAppointment.Id, existingAppointment);

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var existingAppointment = await unitOfWork.AppointmentRepository.GetAsync(id);

            if (existingAppointment == null)
                return NotFound("Appointment not found.");

            await unitOfWork.AppointmentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}