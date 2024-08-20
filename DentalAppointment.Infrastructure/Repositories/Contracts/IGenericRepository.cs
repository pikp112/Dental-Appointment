using DentalAppointment.Core.Models;

namespace DentalAppointment.Infrastructure.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<T> GetAsync(DateTime appointmentDate);

        Task AddAsync(T Entity);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(Guid id, T Entity);
    }
}