using DentalAppointment.Core.Models;

namespace DentalAppointment.Infrastructure.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<T> GetAsync(Guid id);

        Task AddAsync(T Entity);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(T Entity);
    }
}