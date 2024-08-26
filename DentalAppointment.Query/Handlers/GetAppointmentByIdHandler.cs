using AutoMapper;
using DentalAppointment.Core.Dtos;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Query.Queries;
using MediatR;

namespace DentalAppointment.Query.Handlers
{
    public class GetAppointmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        public async Task<AppointmentDto?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetAsync(request.Id);

            return appointment == null ? null : mapper.Map<AppointmentDto>(appointment);
        }
    }
}