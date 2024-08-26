using DentalAppointment.Core.Enums;
using DentalAppointment.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace DentalAppointment.Core.Models
{
    public record AppointmentModel : BaseModel
    {
        [StringLength(200, MinimumLength = 2)]
        public required string PatientName { get; set; }
        [Phone]
        [StringLength(14, MinimumLength = 10)]
        public required string PatientPhoneNumber { get; set; }
        private TreatmentType _treatmentType = TreatmentType.Consultation;
        public TreatmentType TreatmentType
        {
            get => _treatmentType;
            set
            {
                _treatmentType = value;
                Duration = GetDurationByTreatment(value);
            }
        }
        public TimeSpan Duration { get; private set; }
        public bool IsConfirmed { get; set; } = false;
        [StringLength(1000)]
        public string? Notes { get; set; } = string.Empty;
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
    }
}