using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers;

public class SendResetPasswordCommandHandler : ResponseHandler, IRequestHandler<SendResetPasswordCommand, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthenticationService _authenticationService;
    public SendResetPasswordCommandHandler(IStringLocalizer<SharedResources> localization, IAuthenticationService authenticationService) : base(localization)
    {
        _localization = localization;
        _authenticationService = authenticationService;
    }

    public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.SendResetPasswordCode(request.Email);
        switch (result)
        {
            case "UserNameIsExist":
                return BadRequest<string>(_localization[SharedResourcesKey.UserNameIsExist]);
            case "ErrorUpdateUser":
                return BadRequest<string>(_localization[SharedResourcesKey.ErrorUpdateUser]);

            case "FailedToAddNewRoles":
                return BadRequest<string>(_localization[SharedResourcesKey.TryOperationAgain]);
            case "Success":
                return Success<string>(_localization[SharedResourcesKey.Success]);

            default: return BadRequest<string>("");
        }

    }
}