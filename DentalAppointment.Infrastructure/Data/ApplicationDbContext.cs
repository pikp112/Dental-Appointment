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
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.AppointmentDateTime)
                      .IsUnique();

                entity.Property(e => e.TreatmentType)
                      .HasConversion<string>();

                entity.Property(e => e.IsConfirmed)
                      .IsRequired();

                entity.Property(e => e.IsRejected)
                      .IsRequired();

                entity.HasIndex(e => e.PatientPhoneNumber)
                      .IsUnique();

                entity.Property(e => e.Notes)
                      .HasMaxLength(1000);

                entity.HasIndex(e => new { e.AppointmentDateTime, e.PatientPhoneNumber })
                      .IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}