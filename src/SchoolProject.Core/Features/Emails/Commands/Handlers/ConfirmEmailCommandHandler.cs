using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Core.Features.Emails.Commands.Handlers;

public class ConfirmEmailCommandHandler : ResponseHandler, IRequestHandler<ConfirmEmailQuery, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthenticationService _authenticationService;

    public ConfirmEmailCommandHandler(IStringLocalizer<SharedResources> localization, IAuthenticationService authenticationService) : base(localization)
    {
        _localization = localization;
        _authenticationService = authenticationService;
    }

    public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
    {
        var confirmEmail = await _authenticationService.ConfirmEmail(request.UserId, request.Code);
        if (confirmEmail == "ErrorConfirmEmail")
            return BadRequest<string>(_localization[SharedResourcesKey.ErrorConfirmEmail]);
        return Success<string>(_localization[SharedResourcesKey.ConfirmEmailDone]);

    }
}