using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers;

public class UpdateUserRoleQueryHandler : ResponseHandler, IRequestHandler<UpdateUserRolesCommand, Response<string>>
{

    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthorizationService _authorizationService;

    public UpdateUserRoleQueryHandler(IStringLocalizer<SharedResources> localization,
        IAuthorizationService authorizationService) : base(localization)
    {
        _localization = localization;
        _authorizationService = authorizationService;

    }

    public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.UpdateUserRoles(request);
        switch (result)
        {
            case "UserIsNull":
                return NotFound<string>(_localization[SharedResourcesKey.UserIsNotFound]);
            case "FailedToRemoveOldRoles":
                return BadRequest<string>(_localization[SharedResourcesKey.FailedToRemoveOldRole]);
            case "FailedToAddNewRoles":
                return BadRequest<string>(_localization[SharedResourcesKey.FailedToAddNewRole]);
            case "Success":
                return Success<string>(_localization[SharedResourcesKey.Success]);
            case "FailedToUpdateRole":
                return BadRequest<string>(_localization[SharedResourcesKey.Success]);
        }

        return Success<string>(_localization[SharedResourcesKey.Success]);
    }
}