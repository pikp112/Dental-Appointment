namespace DentalAppointment.Entities.Dtos
{
    public record EmailDto
    {
        public string To { get; init; } = string.Empty;
        public string Subject { get; init; } = string.Empty;
        public string Body { get; init; } = string.Empty;
    }
}