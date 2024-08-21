using DentalAppointment.Core.Enums;
using DentalAppointment.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace DentalAppointment.Core.Dtos
{
    public record AppointmentDto
    {
        [Required(ErrorMessage = "Appointment date and time are required.")]
        [FutureDate(1)]
        public required DateTime AppointmentDate { get; init; }

        [Required(ErrorMessage = "Patient name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Patient name must be between 2 and 200 characters.")]
        public required string PatientName { get; init; }

        [Required(ErrorMessage = "Patient phone number is required.")]
        [Phone(ErrorMessage = "The phone number provided is not valid.")]
        [StringLength(14, MinimumLength = 10, ErrorMessage = "Patient phone number must be between 10 and 14 characters.")]
        public required string PatientPhoneNumber { get; init; }

        [Required(ErrorMessage = "Treatment type is required.")]
        public TreatmentType TreatmentType { get; init; } = TreatmentType.Consultation;

        [StringLength(1000, ErrorMessage = "Notes cannot be longer than 1000 characters.")]
        public string? Notes { get; init; } = string.Empty;
    }
}