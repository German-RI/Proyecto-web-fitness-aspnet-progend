using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
namespace ProyectoPROGEND.Services  
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Obtiene la configuración SMTP desde appsettings.json
            var emailSettings = _configuration.GetSection("EmailSettings");

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            mimeMessage.To.Add(MailboxAddress.Parse(email));
            mimeMessage.Subject = subject;

            // Construye el cuerpo del mensaje en HTML
            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            // Conecta al servidor SMTP con la configuración especificada y envía el email
            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings["SmtpUser"], emailSettings["SmtpPass"]);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }

}