using DentalAppointment.Core.Enums;
using DentalAppointment.Core.Models;
using DentalAppointment.Entities.Dtos;
using DentalAppointment.Infrastructure.Data;
using DentalAppointment.Infrastructure.Repositories.Contracts;
using DentalAppointment.Infrastructure.Services.Contracts;
using DentalAppointment.Infrastructure.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quickwire.Attributes;

namespace DentalAppointment.Infrastructure.Repositories.Implementations
{
    [InjectAllInitOnlyProperties]
    public class AppointmentRepository(ApplicationDbContext applicationDbContext, IConfiguration configuration, IEmailService emailService) : GenericRepository<AppointmentModel>(applicationDbContext), IAppointmentRepository
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

            await SendConfirmationEmailToAdminAsync(appointment, "create");

            return appointment;
        }

        public async Task<AppointmentModel> UpdateAppointmentAsync(DateTime actualAppointmentDateTime, DateTime? newAppointmentDateTime, string? patientName, string? patientPhoneNumber, TreatmentType? treatmentType, string? notes, bool? isConfirmed, bool? isRejected)
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

            if (isRejected.HasValue)
                existingAppointment.IsRejected = isRejected.Value;

            if (existingAppointment.IsConfirmed! && existingAppointment.IsRejected!)
                await SendConfirmationEmailToAdminAsync(existingAppointment, "update");

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
            var overlappingAppointments = await applicationDbContext.Appointments
                .Where(a => a.Id != excludedAppointmentId
                            && a.AppointmentDateTime < appointmentDateTime.AddMinutes(30)
                            && a.AppointmentDateTime.AddMinutes(30) > appointmentDateTime)
                .ToListAsync();

            if (overlappingAppointments.Any())
                throw new InvalidOperationException("The appointment overlaps with existing appointments.");
        }

        private async Task SendConfirmationEmailToAdminAsync(AppointmentModel appointment, string actionType, DateTime? oldAppointmentDateTime = null)
        {
            var domain = configuration.GetValue<string>("Domain");

            var confirmationLink = $"{domain}/api/v1/Appointments/confirm/{appointment.Id}?confirm=true";
            var rejectionLink = $"{domain}/api/v1/Appointments/confirm/{appointment.Id}?confirm=false";

            var emailContent = actionType == "create"
                ? EmailTemplates.GetAppointmentCreationTemplate()
                : actionType == "update"
                ? EmailTemplates.GetAppointmentUpdateTemplate()
                : throw new ArgumentException("Invalid action type for email template");

            emailContent = emailContent.Replace("{{appointmentDateTime}}", appointment.AppointmentDateTime.ToString());
            emailContent = emailContent.Replace("{{patientName}}", appointment.PatientName);
            emailContent = emailContent.Replace("{{patientPhoneNumber}}", appointment.PatientPhoneNumber);
            emailContent = emailContent.Replace("{{confirmationLink}}", confirmationLink);
            emailContent = emailContent.Replace("{{rejectionLink}}", rejectionLink);

            if (actionType == "update" && oldAppointmentDateTime.HasValue)
                emailContent = emailContent.Replace("{{oldAppointmentDateTime}}", oldAppointmentDateTime.Value.ToString());

            var emailDto = new EmailDto
            {
                To = configuration.GetValue<string>("EmailUserName") ?? throw new ArgumentNullException("Admin email is null."),
                Subject = actionType == "create" ? "New Appointment Confirmation Request" : "Updated Appointment Confirmation Request",
                Body = emailContent
            };

            await emailService.SendEmailAsync(emailDto);
        }
    }
}