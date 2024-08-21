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
        public async Task<AppointmentModel?> GetByDateAsync(DateTime appointmentDate)
        {
            return await applicationDbContext.Appointments.FirstOrDefaultAsync(app => app.AppointmentDate == appointmentDate);
        }
    }
}