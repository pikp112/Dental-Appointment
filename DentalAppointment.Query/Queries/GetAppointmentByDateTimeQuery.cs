using DentalAppointment.Entities.Responses;
using MediatR;

namespace DentalAppointment.Queries.Queries
{
    public class GetAppointmentByDateTimeQuery(DateTime appointmentDate) : IRequest<AppointmentResponse>
    {
        public DateTime AppointmentDate { get; set; } = appointmentDate;
    }
}