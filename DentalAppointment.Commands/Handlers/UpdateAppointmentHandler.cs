using AutoMapper;
using DentalAppointment.Commands.Commands;
using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using MediatR;

namespace DentalAppointment.Commands.Handlers
{
    public class UpdateAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateAppointmentCommand, AppointmentModel>
    {
        public async Task<AppointmentModel> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.AppointmentRepository.UpdateAppointmentAsync(request.AppointmentDateTime, request.NewAppointmentDateTime, request.PatientName, request.PatientPhoneNumber, request.TreatmentType, request.Notes, request.IsConfirmed, request.IsRejected);

            return mapper.Map<AppointmentModel>(appointment);
        }
    }
}