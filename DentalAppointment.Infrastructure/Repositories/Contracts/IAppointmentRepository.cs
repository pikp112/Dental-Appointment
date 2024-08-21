using DentalAppointment.Core.Models;

namespace DentalAppointment.Infrastructure.Repositories.Contracts
{
    public interface IAppointmentRepository : IGenericRepository<AppointmentModel>
    {
        Task<AppointmentModel?> GetByDateAsync(DateTime appointmentDate);
    }
}