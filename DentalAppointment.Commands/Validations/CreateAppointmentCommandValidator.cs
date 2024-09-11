using FluentValidation;

namespace DentalAppointment.Commands.Commands
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.AppointmentDateTime)
                .NotEmpty()
                .Custom((appointmentDate, context) =>
                {
                    if (appointmentDate < DateTime.UtcNow.AddHours(1))
                        context.AddFailure("The appointment date must be at least 1 hour from now.");

                    var romaniaTime = TimeZoneInfo.ConvertTimeFromUtc(appointmentDate, TimeZoneInfo.FindSystemTimeZoneById("GTB Standard Time"));

                    var startOfWorkDay = new TimeSpan(9, 0, 0);
                    var endOfWorkDay = new TimeSpan(17, 30, 0);

                    if (romaniaTime.TimeOfDay < startOfWorkDay || romaniaTime.TimeOfDay > endOfWorkDay)
                        context.AddFailure($"The appointment time must be between 09:00 and 17:30.");

                    if (romaniaTime.DayOfWeek == DayOfWeek.Saturday || romaniaTime.DayOfWeek == DayOfWeek.Sunday)
                        context.AddFailure("Appointments can only be scheduled from Monday to Friday.");
                });

            RuleFor(x => x.PatientName)
                .NotEmpty().WithMessage("Patient name is required.")
                .Length(4, 100).WithMessage("Patient name must be between 4 and 100 characters.");

            RuleFor(x => x.PatientPhoneNumber)
                .NotEmpty().WithMessage("Patient phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Patient phone number is not in a valid format.");

            RuleFor(x => x.TreatmentType)
                .IsInEnum().WithMessage("Invalid treatment type.");
        }
    }
}