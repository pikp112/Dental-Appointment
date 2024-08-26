using FluentValidation;

namespace DentalAppointment.Commands.Commands
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.AppointmentDate)
                .NotEmpty()
                .Custom((appointmentDate, context) =>
                {
                    if (appointmentDate < DateTime.UtcNow.AddHours(1))
                        context.AddFailure($"The appointment date must be at least 1 hour from now.");
                }); ;

            RuleFor(x => x.PatientName)
                .NotEmpty().WithMessage("Patient name is required.")
                .Length(2, 100).WithMessage("Patient name must be between 2 and 100 characters.");

            RuleFor(x => x.PatientPhoneNumber)
                .NotEmpty().WithMessage("Patient phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Patient phone number is not in a valid format.");

            RuleFor(x => x.TreatmentType)
                .IsInEnum().WithMessage("Invalid treatment type.");
        }
    }
}