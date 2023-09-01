using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.API.Controllers;

[ApiController]
public class EmailController : AppControllerBase
{
    [HttpPost(Router.EmailRoute.SendEmail)]
    public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)
    {
        var response = await Mediator.Send(command);
        return NewResult(response);
    }
}

