using AutoMapper;
using DentalAppointment.Commands.Commands;
using DentalAppointment.Entities.Responses;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using MediatR;

namespace DentalAppointment.Commands.Handlers
{
    public class CreateAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateAppointmentCommand, AppointmentResponse>
    {
        public async Task<AppointmentResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.AppointmentRepository.CreateAppointmentAsync(request.Id, request.AppointmentDateTime, request.PatientName, request.PatientPhoneNumber, request.TreatmentType, request.Notes!);

            return mapper.Map<AppointmentResponse>(appointment);
        }
    }
}