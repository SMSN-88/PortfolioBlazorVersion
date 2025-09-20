using System.Net;
using System.Net.Mail;
using PortfolioBlazorVersion.Shared;
namespace PortfolioBlazorVersion.Pages
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task SendEmailAsync(ContactFormModel model)
        {
            var host = _config["Smtp:Host"];
            var port = int.Parse(_config["Smtp:Port"] ?? "587");
            var user = _config["Smtp:Username"];
            var pass = _config["Smtp:Password"];
            var fromEmail = _config["Smtp:FromEmail"];
            var toEmail = _config["Smtp:ToEmail"];

            using var smtp = new SmtpClient(host)
            {
                Port = port,
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, "Portfolio Contact Form"),
                Subject = $"New Contact From Submission from {model.Name}",
                Body = $"Name: {model.Name}\nEmail: {model.Email}\n\nMessage:\n{model.Message}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(toEmail);

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                try
                {
                    mailMessage.ReplyToList.Add(new MailAddress(model.Email));
                }
                catch { /* ignore invalid reply-to */ }
            }        

            await smtp.SendMailAsync(mailMessage);
        }
    }
}
