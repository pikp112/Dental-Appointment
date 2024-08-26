using AutoMapper;
using DentalAppointment.Commands.Commands;
using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using MediatR;

namespace DentalAppointment.Commands.Handlers
{
    public class CreateAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateAppointmentCommand, AppointmentModel>
    {
        public async Task<AppointmentModel> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.AppointmentRepository.CreateAppointmentAsync(request.Id, request.AppointmentDate, request.PatientName, request.PatientPhoneNumber, request.TreatmentType, request.Notes);

            return mapper.Map<AppointmentModel>(appointment);
        }
    }
}