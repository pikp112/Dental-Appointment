using DentalAppointment.Queries.Queries;
using FluentValidation;

namespace DentalAppointment.Queries.Validations
{
    public class GetAppointmentByDateTimeQueryValidator : AbstractValidator<GetAppointmentByDateTimeQuery>
    {
        public GetAppointmentByDateTimeQueryValidator()
        {
            RuleFor(query => query.AppointmentDate)
                .NotEmpty().WithMessage("Appointment date is required.")
                .Must(BeAValidDate).WithMessage("Appointment date must be a valid date.")
                .Must(BeNotTooFarInPast).WithMessage("Appointment date cannot be more than 2 years in the past.")
                .Must(BeNotTooFarInFuture).WithMessage("Appointment date cannot be more than 2 years in the future.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date != default;
        }

        private bool BeNotTooFarInPast(DateTime date)
        {
            return date >= DateTime.Now.AddYears(-2);
        }

        private bool BeNotTooFarInFuture(DateTime date)
        {
            return date <= DateTime.Now.AddYears(2);
        }
    }
}