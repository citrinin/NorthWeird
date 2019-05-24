using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace NorthWeird.Infrastructure.Mailing
{
    public class EmailSender: IEmailSender
    {

        private readonly string _fromEmail;
        private readonly string _smtpServer;
        private readonly string _password;

        public EmailSender(IConfiguration configuration)
        {
            _fromEmail = configuration.GetValue<string>("EmailSender:Email");
            _password = configuration.GetValue<string>("EmailSender:Password");
            _smtpServer = configuration.GetValue<string>("EmailSender:SmtpServer");
        }

        public async Task SendEmailAsync(string emailAddress, string subject, string message)
        {
            using (var client = GetClient())
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    To = { emailAddress },
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                    
                };

                await client.SendMailAsync(mailMessage);
            }
        }

        private SmtpClient GetClient()
        {
            return new SmtpClient(_smtpServer)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(_fromEmail, _password),
                Port = 587,
                EnableSsl = true
            };
        }
    }
}
