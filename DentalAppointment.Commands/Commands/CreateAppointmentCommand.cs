using DentalAppointment.Core.Enums;
using DentalAppointment.Core.Models;
using MediatR;

namespace DentalAppointment.Commands.Commands
{
    public record CreateAppointmentCommand : BaseModel, IRequest<AppointmentModel>
    {
        public required string PatientName { get; set; }

        public required string PatientPhoneNumber { get; set; }

        public TreatmentType TreatmentType { get; set; } = TreatmentType.Consultation;

        public string? Notes { get; init; } = string.Empty;
    }
}