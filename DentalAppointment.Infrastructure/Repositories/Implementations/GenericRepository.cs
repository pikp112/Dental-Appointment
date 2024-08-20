using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BaseModel
    {
        public async Task AddAsync(T Entity)
        {
            await context.Set<T>()
                    .AddAsync(Entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);

            if (entity != null)
            {
                context.Set<T>().Remove(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<T> GetAsync(Guid id)
            => await context.Set<T>().FindAsync(id);

        public async Task UpdateAsync(Guid id, T Entity)
        {
            var ex_entity = await context.Set<T>().FindAsync(id);
            if (ex_entity != null)
            {
                context.Update(ex_entity);
                await context.SaveChangesAsync();
            }
        }
    }
}