using DentalAppointment.Commands.Commands;
using FluentValidation;

namespace DentalAppointment.Commands.Validations
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentDateTime)
                .NotEmpty().WithMessage("The appointment date time is required.")
                .Must(BeAtLeastOneHourInFuture).WithMessage("The appointment date time could be updated with at least one hour from now.");

            RuleFor(x => x.NewAppointmentDateTime)
                .Must(date => date.HasValue && date.Value >= DateTime.Now.AddHours(1))
                .When(x => x.NewAppointmentDateTime.HasValue)
                .WithMessage("The new appointment date must be at least one hour from now.");

            RuleFor(x => x.PatientName)
                .NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.PatientName)).WithMessage("The patient's name must be at least 4 characters long.")
                .MinimumLength(4).When(x => !string.IsNullOrWhiteSpace(x.PatientName)).WithMessage("The patient's name must be at least 4 characters long.");

            RuleFor(x => x.PatientPhoneNumber)
                .Matches(@"^\+?\d{10,14}$").When(x => !string.IsNullOrWhiteSpace(x.PatientPhoneNumber))
                .WithMessage("The patient's phone number must be a valid number with 10 to 14 numbers.");

            RuleFor(x => x.TreatmentType)
                .IsInEnum().When(x => x.TreatmentType.HasValue)
                .WithMessage("The treatment type is invalid.");

            RuleFor(x => x.IsConfirmed)
               .NotNull().When(x => x.Notes != null).WithMessage("Confirmation status must be specified.");
        }

        private bool BeAtLeastOneHourInFuture(DateTime date)
        {
            return date >= DateTime.Now.AddHours(1);
        }
    }
}