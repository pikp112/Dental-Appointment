using AutoMapper;
using DentalAppointment.Commands.Commands;
using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using MediatR;

namespace DentalAppointment.Commands.Handlers
{
    public class DeleteAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteAppointmentCommand, AppointmentModel>
    {
        public async Task<AppointmentModel> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.AppointmentRepository.DeleteAppointmentAsync(request.AppointmentDate);

            return mapper.Map<AppointmentModel>(appointment);
        }
    }
}