using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Org.BouncyCastle.Crypto.Macs;
using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.Services
{
	public class EmailService
	{
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Karza!", "projectfuen@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("projectfuen@gmail.com", "qpzilvexdqnfzozc");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}

