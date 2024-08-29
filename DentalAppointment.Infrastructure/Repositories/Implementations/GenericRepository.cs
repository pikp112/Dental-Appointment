using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Quickwire.Attributes;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    [InjectAllInitOnlyProperties]
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

        public async Task UpdateAsync(T Entity)
        {
            var ex_entity = await context.Set<T>().FindAsync(Entity.AppointmentDateTime) ?? throw new InvalidOperationException($"Unable to find appointment from {Entity.AppointmentDateTime}");
            context.Update(ex_entity);
            await context.SaveChangesAsync();
        }
    }
}