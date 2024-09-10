using DentalAppointment.Entities.Dtos;
using DentalAppointment.Infrastructure.Services.Contracts;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DentalAppointment.Infrastructure.Services.Implementations
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        public async Task SendEmailAsync(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration.GetValue<string>("EmailUserName")));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    var host = configuration.GetValue<string>("EmailHost");
                    var port = configuration.GetValue<int>("EmailPort");
                    var userName = configuration.GetValue<string>("EmailUserName");
                    var password = configuration.GetValue<string>("EmailPassword");
                    smtp.CheckCertificateRevocation = false;
                    smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);
                    smtp.Authenticate(userName, password);

                    await smtp.SendAsync(email);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error sending email: {ex.Message}");
                }
                finally
                {
                    await smtp.DisconnectAsync(true);
                }
            }
        }
    }
}