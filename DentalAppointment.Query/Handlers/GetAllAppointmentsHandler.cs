using AutoMapper;
using DentalAppointment.Core.Dtos;
using DentalAppointment.Core.Queries;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using MediatR;

namespace DentalAppointment.Core.Handlers
{
    public class GetAllAppointmentsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllAppointmentsQuery, List<AppointmentDto>>
    {
        public async Task<List<AppointmentDto>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await unitOfWork.AppointmentRepository.GetAll();

            if (appointments == null || !appointments.Any())
                throw new InvalidOperationException("No appointments found.");

            return mapper.Map<List<AppointmentDto>>(appointments);
        }
    }
}