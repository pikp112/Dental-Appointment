using DentalAppointment.Entities.Responses;
using MediatR;

namespace DentalAppointment.Core.Queries
{
    public class GetAllAppointmentsQuery : IRequest<List<AppointmentResponse>>
    {
    }
}