using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalAppointment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
    {
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        var appointment = await appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return Ok(appointment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment(AppointmentDto appointmentDto)
    {
        var result = await appointmentService.CreateAppointmentAsync(appointmentDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        return Ok(result.Appointment);
    }
}