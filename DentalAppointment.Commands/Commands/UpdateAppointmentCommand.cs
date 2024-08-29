using DentalAppointment.Core.Enums;
using DentalAppointment.Core.Models;
using MediatR;

namespace DentalAppointment.Commands.Commands
{
    public class UpdateAppointmentCommand : IRequest<AppointmentModel>
    {
        public DateTime AppointmentDateTime { get; set; }
        public DateTime? NewAppointmentDateTime { get; set; }
        public string? PatientName { get; set; }

        public string? PatientPhoneNumber { get; set; }
        public bool? IsConfirmed { get; set; }

        public TreatmentType? TreatmentType { get; set; }

        public string? Notes { get; set; }
    }
}