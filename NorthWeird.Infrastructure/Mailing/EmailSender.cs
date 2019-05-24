using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace NorthWeird.Infrastructure.Mailing
{
    public class EmailSender: IEmailSender
    {
        private const string FromEmail = "nortweird@gmail.com";
        private const string SmtpServer = "smtp.gmail.com";
        private const string Password = "Ghbdtn1!";

        public async Task SendEmailAsync(string emailAddress, string subject, string message)
        {
            using (var client = GetClient())
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(FromEmail),
                    To = { emailAddress },
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                    
                };

                await client.SendMailAsync(mailMessage);
            }
        }

        private static SmtpClient GetClient()
        {
            return new SmtpClient(SmtpServer)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(FromEmail, Password),
                Port = 587,
                EnableSsl = true
            };
        }
    }
}
