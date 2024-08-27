using DentalAppointment.Commands.Commands;
using FluentValidation;

namespace DentalAppointment.Commands.Validations
{
    public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
    {
        public DeleteAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("The appointment date is required.")
                .Must(BeAValidDate).WithMessage("The appointment date must be a valid date.")
                .Must(BeInFuture).WithMessage("The appointment date must be in the future.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date != default;
        }

        private bool BeInFuture(DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}