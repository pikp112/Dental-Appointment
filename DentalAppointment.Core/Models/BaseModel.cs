using System.ComponentModel.DataAnnotations;

namespace DentalAppointment.Core.Models
{
    public record BaseModel
    {
        [Key]
        public Guid Id { get; } = Guid.NewGuid();
    }
}