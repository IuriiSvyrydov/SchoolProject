using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers;

public class ClaimsCommandHandler : ResponseHandler, IRequestHandler<UpdateUserClaimsCommand, Response<string>>
{
    private IStringLocalizer<SharedResources> _localization;
    private readonly IAuthorizationService _authorizationService;

    public ClaimsCommandHandler(IStringLocalizer<SharedResources> localization, IAuthorizationService authorizationService) : base(localization)
    {
        _localization = localization;
        _authorizationService = authorizationService;
    }

    public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.UpdateUserClaims(request);
        switch (result)
        {
            case "UserIsNull":
                return NotFound<string>(_localization[SharedResourcesKey.UserIsNotFound]);
            case "FailedToRemoveClaims":
                return BadRequest<string>(_localization[SharedResourcesKey.FailedToRemoveOldClaims]);
            case "FailedRoAddNewClaims":
                return BadRequest<string>(_localization[SharedResourcesKey.FailedToAddNewClaims]);
            case "FailedToUpdateClaims":
                return BadRequest<string>(_localization[SharedResourcesKey.FailedToUpdateClaims]);


        }

        return Success<string>(_localization[SharedResourcesKey.Success]);
    }
}