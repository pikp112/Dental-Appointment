using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using Quickwire.Attributes;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    [InjectAllInitOnlyProperties]
    public record UnitOfWork(ApplicationDbContext applicationDbContext) : IUnitOfWork
    {
        private IAppointmentRepository _appointmentRepository;

        public IAppointmentRepository AppointmentRepository
        {
            get { return _appointmentRepository ??= new AppointmentRepository(applicationDbContext); }
        }

        public async ValueTask DisposeAsync() => await applicationDbContext.DisposeAsync();

        public async Task SaveChangesAsync() => await applicationDbContext.SaveChangesAsync();
    }
}