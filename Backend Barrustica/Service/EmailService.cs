using MailKit.Net.Smtp;
using MimeKit;

namespace Backend_Barrustica.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly string SmtpHost = "smtp.gmail.com";
        private readonly int SmtpPort = 587;
        private readonly string SmtpUsername = "barrusticataller@gmail.com";
        private readonly string SmtpPassword = "koaz romn ffwz bvwh";

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Barrústica", SmtpUsername));
            mimeMessage.To.Add(new MailboxAddress("", toEmail)); // Destination email address
            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message
            };

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(SmtpHost, SmtpPort, false);
                await client.AuthenticateAsync(SmtpUsername, SmtpPassword);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}