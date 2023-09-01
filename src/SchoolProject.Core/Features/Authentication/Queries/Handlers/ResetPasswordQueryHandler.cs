using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Core.Features.Authentication.Queries.Handlers;

public class ResetPasswordQueryHandler : ResponseHandler, IRequestHandler<ResetPasswordCommand, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IAuthenticationService _authenticationService;
    public ResetPasswordQueryHandler(IStringLocalizer<SharedResources> localization, IStringLocalizer<SharedResources> localizer, IAuthenticationService authenticationService) : base(localization)
    {
        _localizer = localizer;
        _authenticationService = authenticationService;
    }

    public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.ResetPassword(request.Email, request.Password);
        switch (result)
        {
            case "UserNameIsExist":
                return BadRequest<string>(_localizer[SharedResourcesKey.UserNameIsExist]);

            case "FailedToAddNewRoles":
                return BadRequest<string>(_localizer[SharedResourcesKey.InvalidCode]);
            case "Success":
                return Success<string>(_localizer[SharedResourcesKey.Success]);

            default: return BadRequest<string>("");
        }
    }
}