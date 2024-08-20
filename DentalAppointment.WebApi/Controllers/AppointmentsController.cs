using AutoMapper;
using DentalAppointment.Core.Dtos;
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
        }
    }
}