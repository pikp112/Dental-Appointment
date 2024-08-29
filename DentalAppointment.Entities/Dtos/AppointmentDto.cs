using DentalAppointment.Core.Enums;

namespace DentalAppointment.Core.Dtos
{
    public record AppointmentDto
    {
        public DateTime AppointmentDateTime { get; init; }
        public string PatientName { get; init; }
        public string PatientPhoneNumber { get; init; }
        public bool IsConfirmed { get; init; }
        public TreatmentType TreatmentType { get; init; }
        public string Notes { get; init; }
    }
}