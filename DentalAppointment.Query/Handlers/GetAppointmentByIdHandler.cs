using AutoMapper;
using DentalAppointment.Core.Dtos;
using DentalAppointment.Entities.Responses;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Query.Queries;
using MediatR;

namespace DentalAppointment.Query.Handlers
{
    public class GetAppointmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAppointmentByIdQuery, AppointmentResponse>
    {
        public async Task<AppointmentResponse?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetAsync(request.Id);

            return appointment == null ? null : mapper.Map<AppointmentResponse>(appointment);
        }
    }
}