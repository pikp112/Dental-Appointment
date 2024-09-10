using DentalAppointment.Entities.Dtos;

namespace DentalAppointment.Infrastructure.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto request);
    }
}