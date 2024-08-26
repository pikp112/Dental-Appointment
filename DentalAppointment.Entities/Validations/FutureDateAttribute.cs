using System.ComponentModel.DataAnnotations;

namespace DentalAppointment.Core.Validations
{
    public class FutureDateAttribute(int hoursFromNow) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dateTime)
                throw new ValidationException("The value must be of type DateTime.");

            if (dateTime < DateTime.UtcNow.AddHours(hoursFromNow))
                return new ValidationResult($"The appointment date must be at least {hoursFromNow} hour(s) from now.");

            return ValidationResult.Success;
        }
    }
}