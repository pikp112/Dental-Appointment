using DentalAppointment.Core.Enums;

namespace DentalAppointment.Entities.Responses
{
    public record AppointmentResponse
    {
        public required Guid Id { get; init; }
        public required DateTime AppointmentDateTime { get; init; }
        public required string PatientName { get; init; }
        public required string PatientPhoneNumber { get; init; }
        public required TreatmentType TreatmentType { get; init; }
        public required TimeSpan Duration { get; init; }
        public required bool IsConfirmed { get; init; }
        public required bool IsRejected { get; init; }
        public required string Notes { get; init; }
    }
}