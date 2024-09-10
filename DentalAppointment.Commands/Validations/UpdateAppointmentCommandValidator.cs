using DentalAppointment.Commands.Commands;
using FluentValidation;
using System;

namespace DentalAppointment.Commands.Validations
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentDateTime)
                .NotEmpty().WithMessage("The appointment date time is required.")
                .Custom((appointmentDate, context) =>
                {
                    if (appointmentDate < DateTime.UtcNow.AddHours(1))
                        context.AddFailure("The appointment date must be at least 1 hour from now.");

                    var romaniaTime = TimeZoneInfo.ConvertTimeFromUtc(appointmentDate, TimeZoneInfo.FindSystemTimeZoneById("GTB Standard Time"));

                    var startOfWorkDay = new TimeSpan(9, 0, 0);
                    var endOfWorkDay = new TimeSpan(17, 30, 0);

                    if (romaniaTime.TimeOfDay < startOfWorkDay || romaniaTime.TimeOfDay > endOfWorkDay)
                        context.AddFailure($"The appointment time must be between 09:00 and 17:30.");
                });

            RuleFor(x => x.NewAppointmentDateTime)
                 .Custom((newAppointmentDate, context) =>
                 {
                     if (newAppointmentDate.HasValue)
                     {
                         if (newAppointmentDate.Value < DateTime.UtcNow.AddHours(1))
                             context.AddFailure("The new appointment date must be at least 1 hour from now.");

                         var romaniaTime = TimeZoneInfo.ConvertTimeFromUtc(newAppointmentDate.Value, TimeZoneInfo.FindSystemTimeZoneById("GTB Standard Time"));

                         var startOfWorkDay = new TimeSpan(9, 0, 0);
                         var endOfWorkDay = new TimeSpan(17, 30, 0);

                         if (romaniaTime.TimeOfDay < startOfWorkDay || romaniaTime.TimeOfDay > endOfWorkDay)
                             context.AddFailure("The new appointment time must be between 09:00 and 17:30.");
                     }
                 })
                .When(x => x.NewAppointmentDateTime.HasValue);

            RuleFor(x => x.PatientName)
                .NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.PatientName)).WithMessage("The patient's name must be at least 4 characters long.")
                .MinimumLength(4).When(x => !string.IsNullOrWhiteSpace(x.PatientName)).WithMessage("The patient's name must be at least 4 characters long.");

            RuleFor(x => x.PatientPhoneNumber)
                .Matches(@"^\+?\d{10,14}$").When(x => !string.IsNullOrWhiteSpace(x.PatientPhoneNumber))
                .WithMessage("The patient's phone number must be a valid number with 10 to 14 digits.");

            RuleFor(x => x.TreatmentType)
                .IsInEnum().When(x => x.TreatmentType.HasValue)
                .WithMessage("The treatment type is invalid.");

            RuleFor(x => x.IsConfirmed)
               .NotNull().When(x => x.Notes != null).WithMessage("Confirmation status must be specified.");
        }
    }
}