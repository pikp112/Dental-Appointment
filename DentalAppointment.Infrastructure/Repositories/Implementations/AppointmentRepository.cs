using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    public class AppointmentRepository(ApplicationDbContext applicationDbContext) : GenericRepository<AppointmentModel>(applicationDbContext), IAppointmentRepository
    {
    }
}