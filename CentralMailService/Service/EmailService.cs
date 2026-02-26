using CentralMailService.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net.Mail;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CentralMailService.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailQueue email, SmtpSetting smtp);
    }

    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(EmailQueue email, SmtpSetting smtp)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtp.FromName, smtp.FromEmail));
                message.To.Add(MailboxAddress.Parse(email.ToEmail));
                if (!string.IsNullOrEmpty(email.CcEmail))
                    message.Cc.Add(MailboxAddress.Parse(email.CcEmail));
                if (!string.IsNullOrEmpty(email.BccEmail))
                    message.Bcc.Add(MailboxAddress.Parse(email.BccEmail));
                message.Subject = email.Subject;
                message.Body = new TextPart("html") { Text = email.Body };

                using var client = new SmtpClient();
                await client.ConnectAsync(smtp.SmtpHost, smtp.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtp.SmtpUser, smtp.SmtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}