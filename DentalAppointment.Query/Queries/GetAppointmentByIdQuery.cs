using DentalAppointment.Core.Dtos;
using DentalAppointment.Entities.Responses;
using MediatR;

namespace DentalAppointment.Query.Queries
{
    public class GetAppointmentByIdQuery(Guid id) : IRequest<AppointmentResponse>
    {
        public Guid Id { get; } = id;
    }
}