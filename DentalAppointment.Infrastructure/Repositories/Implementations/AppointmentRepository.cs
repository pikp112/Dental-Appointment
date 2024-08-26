using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Quickwire.Attributes;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    [InjectAllInitOnlyProperties]
    public class AppointmentRepository(ApplicationDbContext applicationDbContext) : GenericRepository<AppointmentModel>(applicationDbContext), IAppointmentRepository
    {
        public async Task<IReadOnlyCollection<AppointmentModel>> GetAll()
        {
            return await applicationDbContext.Appointments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AppointmentModel?> GetByDateAsync(DateTime appointmentDate)
        {
            return await applicationDbContext.Appointments
                .AsNoTracking()
                .FirstOrDefaultAsync(app => app.AppointmentDate == appointmentDate);
        }
    }
}