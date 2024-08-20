using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork(ApplicationDbContext applicationDbContext) : IUnitOfWork
    {
        public void Dispose() => applicationDbContext.Dispose();

        public async Task SaveChangesAsync() => await applicationDbContext.SaveChangesAsync();
    }
}