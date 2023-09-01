using MailKit.Net.Smtp;
using MimeKit;


namespace SchoolProject.Services.Implements;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }
    public async Task<string> SendEmail(string email, string message, string? reason)
    {
        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                var bodyBuilder = new BodyBuilder()
                {
                    HtmlBody = $"{message}",
                    TextBody = "Welcome"
                };
                var Message = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody()
                };
                Message.From.Add(new MailboxAddress("Developer Team", _emailSettings.FromEmail));
                Message.To.Add(new MailboxAddress("testing", email));
                Message.Subject = reason == null ? "No submitted" : reason;
                await client.SendAsync(Message);
                await client.DisconnectAsync(true);
            }

            return "Success";
        }
        catch (Exception e)
        {
            return "Failed";
        }
    }
}