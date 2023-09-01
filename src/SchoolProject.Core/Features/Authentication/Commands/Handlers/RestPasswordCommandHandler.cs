using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers;

public class RestPasswordCommandHandler : ResponseHandler, IRequestHandler<ResetPasswordCommand, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IAuthenticationService _authenticationService;
    public RestPasswordCommandHandler(IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _localizer = localizer;
    }

    public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.ResetPassword(request.Email, request.Password);
        switch (result)
        {
            case "UserNotFound": return BadRequest<string>(_localizer[SharedResourcesKey.UserIsNotFound]);
            case "Failed": return BadRequest<string>(_localizer[SharedResourcesKey.InvalidCode]);
            case "Success": return Success<string>("");
            default: return BadRequest<string>(_localizer[SharedResourcesKey.InvalidCode]);

        }
    }
}