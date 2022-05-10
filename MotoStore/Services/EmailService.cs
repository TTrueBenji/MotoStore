using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MotoStore.Services.Abstractions;
using MotoStore.Services.Senders;

namespace MotoStore.Services
{
    public class EmailService : BaseSender, IEmailService
    {
        
        public EmailService(string host, int port, string @from, string password, bool useSsl) 
            : base(host, port, @from, password, useSsl){}
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            // Здесь указываем ящик с которого будет происходить отправка
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", From)); 
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(Host, Port, true);
            //Данные для аутентификации ящика с которого отправляется письмо
            await client.AuthenticateAsync(From, Password);
            await client.SendAsync(emailMessage);
 
            await client.DisconnectAsync(UseSSL);
        }
    }
}