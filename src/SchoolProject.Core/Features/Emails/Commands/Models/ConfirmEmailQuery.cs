namespace SchoolProject.Core.Features.Emails.Commands.Models;

public class ConfirmEmailQuery : IRequest<Response<string>>
{
    public int UserId { get; set; }
    public string Code { get; set; }
}