using DentalAppointment.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DentalAppointment.Core.Dtos
{
    public record AppointmentDto
    {
        public required DateTime AppointmentDate { get; init; }
        [StringLength(200, MinimumLength = 2)]
        public required string PatientName { get; init; }
        [Phone]
        [StringLength(14, MinimumLength = 10)]
        public required string PatientPhoneNumber { get; init; }
        public TreatmentType TreatmentType { get; init; } = TreatmentType.Consultation;
        [StringLength(1000)]
        public string? Notes { get; init; } = string.Empty;
    }
}