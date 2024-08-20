using AutoMapper;
using DentalAppointment.Core.Dtos;
using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DentalAppointment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                return BadRequest("No candidate data provided.");

            if (!TryValidateModel(appointmentDto))
                return BadRequest(ModelState);

            var existingAppoint = await unitOfWork.AppointmentRepository.GetAsync(appointmentDto.AppointmentDate);

            if (existingAppoint != null)
                return Conflict("Sorry but an appointment already exist.");

            var entity = mapper.Map<AppointmentModel>(appointmentDto);

            await unitOfWork.AppointmentRepository.AddAsync(entity);

            return CreatedAtAction(nameof(CreateAppointment), new { Appointment = entity.AppointmentDate }, entity);
        }
    }
}