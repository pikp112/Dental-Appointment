using DentalAppointment.Commands.Commands;
using FluentValidation;

namespace DentalAppointment.Commands.Validations
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(x => x.ActualAppointmentDate)
                .NotEmpty().WithMessage("The actual appointment date is required.")
                .Must(BeAValidDate).WithMessage("The actual appointment date must be a valid date in the past.");

            RuleFor(x => x.NewAppointmentDate)
                .Must(BeAValidFutureDate).When(x => x.NewAppointmentDate.HasValue)
                .WithMessage("The new appointment date must be a valid date in the future.");

            RuleFor(x => x.PatientName)
                .NotEmpty().WithMessage("The patient's name is required.")
                .MinimumLength(2).WithMessage("The patient's name must be at least 2 characters long.");

            RuleFor(x => x.PatientPhoneNumber)
                .NotEmpty().WithMessage("The patient's phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("The patient's phone number must be a valid number with 10 to 15 digits.");

            RuleFor(x => x.TreatmentType)
                .IsInEnum().WithMessage("The treatment type is invalid.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date != default;
        }

        private bool BeAValidFutureDate(DateTime? date)
        {
            return date.HasValue && date.Value > DateTime.Now;
        }
    }
}