using System.ComponentModel.DataAnnotations;

namespace DentalAppointment.Core.Models
{
    public record BaseModel
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        public required DateTime AppointmentDateTime { get; set; }
    }
}