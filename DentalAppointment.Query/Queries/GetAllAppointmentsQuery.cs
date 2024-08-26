using DentalAppointment.Core.Dtos;
using MediatR;

namespace DentalAppointment.Core.Queries
{
    public class GetAllAppointmentsQuery : IRequest<List<AppointmentDto>>
    {
    }
}