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
            var existingAppointment = await applicationDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentDate == appointmentDateTime);

            if (existingAppointment != null)
                throw new InvalidOperationException("Sorry but an appointment already exist.");

            var appointment = new AppointmentModel
            {
                Id = appointmentId,
                AppointmentDate = appointmentDateTime,
                PatientName = patientName,
                PatientPhoneNumber = patientPhoneNumber,
                TreatmentType = treatmentType,
                Notes = notes
            };

            await applicationDbContext.AddAsync(appointment);

            await applicationDbContext.SaveChangesAsync();

            return appointment;
        }

        public async Task<AppointmentModel> UpdateAppointmentAsync(DateTime actualAppointmentDateTime, DateTime? newAppointmentDateTime, string patientName, string patientPhoneNumber, TreatmentType treatmentType, string notes)
        {
            var existingAppointment = await applicationDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentDate == actualAppointmentDateTime)
                ?? throw new InvalidOperationException($"Sorry an appointment at {actualAppointmentDateTime} doesn't exist.");

            existingAppointment.AppointmentDate = newAppointmentDateTime ?? actualAppointmentDateTime;
            existingAppointment.PatientName = patientName;
            existingAppointment.PatientPhoneNumber = patientPhoneNumber;
            existingAppointment.TreatmentType = treatmentType;
            existingAppointment.Notes = notes;

            applicationDbContext.Update(existingAppointment);

            await applicationDbContext.SaveChangesAsync();

            return existingAppointment;
        }

        public async Task<AppointmentModel> DeleteAppointmentAsync(DateTime appointmentDateTime)
        {
            var existingAppointment = await applicationDbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentDate == appointmentDateTime)
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
                .FirstOrDefaultAsync(app => app.AppointmentDate == appointmentDate);
        }
    }
}