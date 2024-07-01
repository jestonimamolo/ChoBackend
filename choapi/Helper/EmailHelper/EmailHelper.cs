using System.Net.Mail;
using System.Net;
using System.Data.SqlTypes;

namespace choapi.Helper
{
    public class EmailHelper : IEmailHelper
    {
        private readonly SmtpClient _smtpClient;

        private readonly string _host = "smtp.office365.com";
        private readonly string _email = "mj.saile@gukodigital.com";
        private readonly string _port = "587";

        public EmailHelper(IConfiguration configuration)
        {
            _smtpClient = new SmtpClient(_host)
            {
                Port = Convert.ToInt32(_port),
                Credentials = new NetworkCredential(_email, "zzzzzzzzzzzz"),
                EnableSsl = true // You can set it to false if you don't want to use SSL
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_email);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true; // Set to true if you're sending HTML email

            await _smtpClient.SendMailAsync(message);
        }
    }
}
