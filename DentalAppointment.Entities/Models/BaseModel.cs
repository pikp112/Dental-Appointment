using DentalAppointment.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace DentalAppointment.Core.Models
{
    public record BaseModel
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();
        [FutureDate(1)]
        public required DateTime AppointmentDate { get; set; }
    }
}