using DentalAppointment.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalAppointment.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public virtual DbSet<AppointmentModel> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentModel>(entity =>
            {
                entity.Property(e => e.TreatmentType)
                    .HasConversion<string>();

                entity.Property(e => e.Duration)
                   .IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}