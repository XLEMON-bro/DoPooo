using DoPooo.Services.IServices;
using MailKit.Security;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MimeKit.Text;

namespace DoPooo.Services
{
    public class Mail : IServices.IMailService
    {
        public void Send(string to, string subject, string html, string from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com",587,SecureSocketOptions.StartTls);
            smtp.Authenticate("name", "password");
            smtp.Send(email);
            smtp.Disconnect(true);
            /* Изменить параметры Connection Auntificate Email.From.Add(from)
             *
             */
        }
    }
}
