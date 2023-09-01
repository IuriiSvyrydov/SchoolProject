
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers;

public class RefreshTokenCommandHandler : ResponseHandler, IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthenticationService _authenticationService;
    private readonly UserManager<User> _userManager;
    public RefreshTokenCommandHandler(IStringLocalizer<SharedResources> localization, IAuthenticationService authenticationService, UserManager<User> userManager) : base(localization)
    {
        _localization = localization;
        _authenticationService = authenticationService;
        _userManager = userManager;
    }

    public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
        var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
        switch (userIdAndExpireDate)
        {
            case ("AlgorithmIsWrong", null):
                return Unauthorized<JwtAuthResult>(_localization[SharedResourcesKey.AlgorithmIsWrong]);
                break;
            case ("TokenIsNotExpired", null):
                return Unauthorized<JwtAuthResult>(_localization[SharedResourcesKey.TokenIsNotExpired]);
                break;
            case ("RefreshTokenIsNotFound", null):
                return Unauthorized<JwtAuthResult>(_localization[SharedResourcesKey.RefreshTokenIsNotFound]);
                break;
        }

        var (userId, expiryDate) = userIdAndExpireDate;
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return NotFound<JwtAuthResult>(_localization[SharedResourcesKey.NotFound]);
        }
        var result = await _authenticationService.GetRefreshToken(user, jwtToken, request.RefreshToken, expiryDate);
        return Success(result);
    }
}