namespace choapi.Helper
{
    public interface IEmailHelper
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
