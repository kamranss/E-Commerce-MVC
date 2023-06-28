using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace AllUp2.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(string to, string subject, string body)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Smtp:FromAddress").Value)); // this our email
            email.To.Add(MailboxAddress.Parse(to)); // this is cient email front will pass this email to us
            email.Subject = subject; // some information 
            email.Body = new TextPart(TextFormat.Html) { Text = body }; // changing body to html 

            // send email
            using SmtpClient smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("Smtp:Server").Value, int.Parse(_configuration.GetSection("Smtp:Port").Value), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("Smtp:FromAddress").Value, _configuration.GetSection("Smtp:Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
