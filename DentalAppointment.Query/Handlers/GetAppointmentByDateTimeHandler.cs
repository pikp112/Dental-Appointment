using AutoMapper;
using DentalAppointment.Core.Dtos;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Queries.Queries;
using MediatR;

namespace DentalAppointment.Queries.Handlers
{
    public class GetAppointmentByDateTimeHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAppointmentByDateTimeQuery, AppointmentDto>
    {
        public async Task<AppointmentDto> Handle(GetAppointmentByDateTimeQuery request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetByDateAsync(request.AppointmentDate);

            return mapper.Map<AppointmentDto>(appointment);
        }
    }
}