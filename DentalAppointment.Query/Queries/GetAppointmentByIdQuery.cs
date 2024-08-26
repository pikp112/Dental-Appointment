using DentalAppointment.Core.Dtos;
using MediatR;

namespace DentalAppointment.Query.Queries
{
    public class GetAppointmentByIdQuery(Guid id) : IRequest<AppointmentDto>
    {
        public Guid Id { get; } = id;
    }
}