namespace DentalAppointment.Infrastructure.Repositories.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAppointmentRepository AppointmentRepository { get; }

        Task SaveChangesAsync();
    }
}