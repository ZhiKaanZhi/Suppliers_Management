using System.Net.Mail;
using System.Net;
using WebApplication1.Configuration;
using WebApplication1.Services.ServiceInterfaces;
using Microsoft.Extensions.Options;

namespace WebApplication1.Services
{
    public class EmailNotificationService : INotificationService
    {

        private readonly IOptions<EmailSettings> _emailSettings;

        public EmailNotificationService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendWelcomeEmailAsync(string? email, string? supplierName)
        {
            using var smtp = new SmtpClient(_emailSettings.Value.MailServer, _emailSettings.Value.MailPort)
            {
                Credentials = new NetworkCredential(_emailSettings.Value.Username, _emailSettings.Value.Password),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(_emailSettings.Value.SenderEmail, _emailSettings.Value.SenderName),
                Subject = "Welcome to Our Service",
                Body = $"Hello {supplierName}, welcome to our platform.",
                IsBodyHtml = true
            };

            if(email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            message.To.Add(email);
            await smtp.SendMailAsync(message);
        }
    }
}
