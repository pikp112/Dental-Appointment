using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Infrastructure.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Quickwire.Attributes;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    [InjectAllInitOnlyProperties]
    public record UnitOfWork(ApplicationDbContext applicationDbContext, IConfiguration configuration, IEmailService emailService) : IUnitOfWork
    {
        private IAppointmentRepository _appointmentRepository;

        public IAppointmentRepository AppointmentRepository
        {
            get { return _appointmentRepository ??= new AppointmentRepository(applicationDbContext, configuration, emailService); }
        }

        public async ValueTask DisposeAsync() => await applicationDbContext.DisposeAsync();

        public async Task SaveChangesAsync() => await applicationDbContext.SaveChangesAsync();
    }
}