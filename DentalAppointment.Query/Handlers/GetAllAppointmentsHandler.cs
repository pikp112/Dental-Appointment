using AutoMapper;
using DentalAppointment.Core.Queries;
using DentalAppointment.Entities.Responses;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using MediatR;

namespace DentalAppointment.Core.Handlers
{
    public class GetAllAppointmentsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllAppointmentsQuery, List<AppointmentResponse>>
    {
        public async Task<List<AppointmentResponse>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await unitOfWork.AppointmentRepository.GetAll();

            if (appointments == null || !appointments.Any())
                throw new InvalidOperationException("No appointments found.");

            return mapper.Map<List<AppointmentResponse>>(appointments);
        }
    }
}