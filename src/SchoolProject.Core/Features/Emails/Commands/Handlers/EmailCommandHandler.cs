using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Core.Features.Emails.Commands.Handlers;

public class EmailCommandHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>

{
    private readonly IEmailService _emailService;
    private readonly IStringLocalizer<SharedResources> _localization;
    public EmailCommandHandler(IStringLocalizer<SharedResources> localization, IEmailService emailService) : base(localization)
    {
        _localization = localization;
        _emailService = emailService;
    }

    public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await _emailService.SendEmail(request.Email, request.Message, null);
        if (response == "Success") return Success<string>("");
        return BadRequest<string>(_localization[SharedResourcesKey.SendEmailFailed]);
    }
}