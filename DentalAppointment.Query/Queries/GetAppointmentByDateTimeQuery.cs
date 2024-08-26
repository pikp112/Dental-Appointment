using DentalAppointment.Core.Dtos;
using MediatR;

namespace DentalAppointment.Queries.Queries
{
    public class GetAppointmentByDateTimeQuery(DateTime appointmentDate) : IRequest<AppointmentDto>
    {
        public DateTime AppointmentDate { get; set; } = appointmentDate;
    }
}