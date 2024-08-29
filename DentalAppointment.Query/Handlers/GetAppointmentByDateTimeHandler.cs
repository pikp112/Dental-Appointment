using AutoMapper;
using DentalAppointment.Core.Dtos;
using DentalAppointment.Entities.Responses;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Queries.Queries;
using MediatR;

namespace DentalAppointment.Queries.Handlers
{
    public class GetAppointmentByDateTimeHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAppointmentByDateTimeQuery, AppointmentResponse>
    {
        public async Task<AppointmentResponse> Handle(GetAppointmentByDateTimeQuery request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetByDateAsync(request.AppointmentDate);

            return mapper.Map<AppointmentResponse>(appointment);
        }
    }
}