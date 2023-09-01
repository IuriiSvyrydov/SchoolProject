using SchoolProject.Core.Features.Authentication.Queries.Models;

namespace SchoolProject.Core.Features.Authentication.Queries.Handlers;

public class AuthorizeUserCommandHandler : ResponseHandler, IRequestHandler<AuthorizeUserQuery, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthenticationService _authenticationService;
    public AuthorizeUserCommandHandler(IStringLocalizer<SharedResources> localization,
         IAuthenticationService authenticationService) : base(localization)
    {
        _localization = localization;
        _authenticationService = authenticationService;
    }

    public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.ValidateToken(request.AccessToken);
        if (result == "NotExpired")
            return Success(result);
        return Unauthorized<string>(_localization[SharedResourcesKey.TokenIsExpired]);
    }
}