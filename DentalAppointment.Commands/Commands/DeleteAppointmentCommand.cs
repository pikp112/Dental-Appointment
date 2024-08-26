using DentalAppointment.Core.Models;
using MediatR;

namespace DentalAppointment.Commands.Commands
{
    public class DeleteAppointmentCommand(DateTime appointmentDateTime) : IRequest<AppointmentModel>
    {
        public DateTime AppointmentDate { get; set; } = appointmentDateTime;
    }
}