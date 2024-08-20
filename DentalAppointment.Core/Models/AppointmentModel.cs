using DentalAppointment.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DentalAppointment.Core.Models
{
    public record AppointmentModel : BaseModel
    {
        public required DateTime AppointmentDate { get; init; }
        [StringLength(200, MinimumLength = 2)]
        public required string PatientName { get; init; }
        [Phone]
        [StringLength(14, MinimumLength = 10)]
        public required string PatientPhoneNumber { get; init; }
        public TreatmentType TreatmentType { get; init; } = TreatmentType.Consultation;
        public TimeSpan Duration => GetDurationByTreatment(TreatmentType);

        private static TimeSpan GetDurationByTreatment(TreatmentType treatmentType)
        {
            return treatmentType switch
            {
                TreatmentType.Consultation => TimeSpan.FromMinutes(30),
                TreatmentType.Cleaning => TimeSpan.FromMinutes(45),
                TreatmentType.Filling => TimeSpan.FromMinutes(60),
                TreatmentType.Extraction => TimeSpan.FromMinutes(90),
                TreatmentType.RootCanal => TimeSpan.FromMinutes(120),
                TreatmentType.Whitening => TimeSpan.FromMinutes(75),
                TreatmentType.Checkup => TimeSpan.FromMinutes(30),
                TreatmentType.Orthodontics => TimeSpan.FromMinutes(60),
                _ => TimeSpan.FromMinutes(30)
            };
        }
        public bool IsConfirmed { get; init; } = false;
        [StringLength(1000)]
        public string? Notes { get; init; } = string.Empty;
    }
}