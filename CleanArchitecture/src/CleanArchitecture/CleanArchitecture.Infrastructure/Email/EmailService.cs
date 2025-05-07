using CleanArchitecture.Application.Abstractions.Email;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace CleanArchitecture.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        public GmailSettings _gmailSettings;

        public EmailService(IOptions<GmailSettings> gmailSettings)
        {
            _gmailSettings = gmailSettings.Value;
        }

        public void Send(string recipient, string subject, string body)
        {
            try
            {

                var fromEmail = _gmailSettings.Username;
                var fromPassword = _gmailSettings.Password;

                var message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.Subject = subject;
                message.To.Add(new MailAddress(recipient));
                message.Body = body;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = _gmailSettings.Port,
                    Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending email", ex);
            }
        }
    }
}
