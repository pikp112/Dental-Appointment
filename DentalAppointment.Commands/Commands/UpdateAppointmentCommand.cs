using DentalAppointment.Core.Enums;
using DentalAppointment.Core.Models;
using MediatR;

namespace DentalAppointment.Commands.Commands
{
    public class UpdateAppointmentCommand : IRequest<AppointmentModel>
    {
        public DateTime ActualAppointmentDate { get; set; }
        public DateTime? NewAppointmentDate { get; set; }
        public string PatientName { get; set; }

        public string PatientPhoneNumber { get; set; }

        public TreatmentType TreatmentType { get; set; } = TreatmentType.Consultation;

        public string? Notes { get; set; } = string.Empty;
    }
}