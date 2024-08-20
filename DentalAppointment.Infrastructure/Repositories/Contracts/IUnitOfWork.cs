namespace DentalAppointment.Infrastructure.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository AppointmentRepository { get; }

        Task SaveChangesAsync();
    }
}