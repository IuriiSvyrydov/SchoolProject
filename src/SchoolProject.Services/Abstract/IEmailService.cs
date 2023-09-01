namespace SchoolProject.Services.Abstract;

public interface IEmailService
{
    Task<string> SendEmail(string email, string message, string? reason);
}