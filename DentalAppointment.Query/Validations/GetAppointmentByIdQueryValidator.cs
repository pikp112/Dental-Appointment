using DentalAppointment.Query.Queries;
using FluentValidation;

namespace DentalAppointment.Queries.Validations
{
    public class GetAppointmentByIdQueryValidator : AbstractValidator<GetAppointmentByIdQuery>
    {
        public GetAppointmentByIdQueryValidator()
        {
            RuleFor(query => query.Id)
                .NotEmpty().WithMessage("The appointment ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("The appointment ID must be a valid GUID.");
        }
    }
}