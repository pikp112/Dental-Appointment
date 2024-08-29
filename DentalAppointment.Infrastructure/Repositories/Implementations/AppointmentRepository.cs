using Azure.Core;
using DentalAppointment.Core.Enums;
using DentalAppointment.Core.Models;
using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Quickwire.Attributes;
using System;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    [InjectAllInitOnlyProperties]
    public class AppointmentRepository(ApplicationDbContext applicationDbContext) : GenericRepository<AppointmentModel>(applicationDbContext), IAppointmentRepository
    {
        public async Task<AppointmentModel> CreateAppointmentAsync(Guid appointmentId, DateTime appointmentDateTime, string patientName, string patientPhoneNumber, TreatmentType treatmentType, string notes)
        {
            await CheckForOverlappingAppointmentsAsync(appointmentDateTime, null);

            var appointment = new AppointmentModel
            {
                Id = appointmentId,
                AppointmentDateTime = appointmentDateTime,
                PatientName = patientName,
                PatientPhoneNumber = patientPhoneNumber,
                TreatmentType = treatmentType,
                Notes = notes
            };

            await applicationDbContext.AddAsync(appointment);

            await applicationDbContext.SaveChangesAsync();

            return appointment;
        }

        public async Task<AppointmentModel> UpdateAppointmentAsync(DateTime actualAppointmentDateTime, DateTime? newAppointmentDateTime, string? patientName, string? patientPhoneNumber, TreatmentType? treatmentType, string? notes, bool? isConfirmed)
        {
            var existingAppointment = await applicationDbContext.Appointments
                .FirstOrDefaultAsync(x => x.AppointmentDateTime == actualAppointmentDateTime)
                ?? throw new InvalidOperationException($"Sorry an appointment at {actualAppointmentDateTime} doesn't exist.");

            await CheckForOverlappingAppointmentsAsync(newAppointmentDateTime ?? actualAppointmentDateTime, existingAppointment.Id);

            if (newAppointmentDateTime.HasValue)
                existingAppointment.AppointmentDateTime = (DateTime)newAppointmentDateTime;

            if (!string.IsNullOrWhiteSpace(patientName))
                existingAppointment.PatientName = patientName;

            if (!string.IsNullOrWhiteSpace(patientPhoneNumber))
                existingAppointment.PatientPhoneNumber = patientPhoneNumber;

            if (treatmentType.HasValue)
                existingAppointment.TreatmentType = treatmentType.Value;

            if (!string.IsNullOrWhiteSpace(notes))
                existingAppointment.Notes = notes;

            if (isConfirmed.HasValue)
                existingAppointment.IsConfirmed = isConfirmed.Value;

            applicationDbContext.Update(existingAppointment);

            await applicationDbContext.SaveChangesAsync();

            return existingAppointment;
        }

        public async Task<AppointmentModel> DeleteAppointmentAsync(DateTime appointmentDateTime)
        {
            var existingAppointment = await applicationDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentDateTime == appointmentDateTime)
                    ?? throw new InvalidOperationException($"Sorry an appointment at {appointmentDateTime} doesn't exist.");

            applicationDbContext.Remove(existingAppointment);

            await applicationDbContext.SaveChangesAsync();

            return existingAppointment;
        }

        public async Task<IReadOnlyCollection<AppointmentModel>> GetAll()
        {
            return await applicationDbContext.Appointments
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<AppointmentModel?> GetByDateAsync(DateTime appointmentDate)
        {
            return await applicationDbContext.Appointments
                .AsNoTracking()
                .FirstOrDefaultAsync(app => app.AppointmentDateTime == appointmentDate);
        }

        private async Task CheckForOverlappingAppointmentsAsync(DateTime appointmentDateTime, Guid? excludedAppointmentId)
        {
            var endTime = appointmentDateTime.Add(TimeSpan.FromMinutes(30));

            var overlappingAppointments = await applicationDbContext.Appointments
                .Where(a => a.Id != excludedAppointmentId
                            && a.AppointmentDateTime < endTime
                            && a.AppointmentDateTime.Add(TimeSpan.FromMinutes(30)) > appointmentDateTime)
                .ToListAsync();

            if (overlappingAppointments.Any())
                throw new InvalidOperationException("The appointment overlaps with existing appointments.");
        }
    }
}