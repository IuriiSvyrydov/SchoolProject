using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers;

public class DeleteRoleCommandHandler : ResponseHandler, IRequestHandler<DeleteRoleCommand, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthorizationService _authorizationService;
    public DeleteRoleCommandHandler(IStringLocalizer<SharedResources> localization, IAuthorizationService authorizationService) : base(localization)
    {
        _localization = localization;
        _authorizationService = authorizationService;
    }

    public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.DeleteRoleAsync(request.Id);
        if (result == "NotFound") return NotFound<string>();
        else if (result == "Used") return BadRequest<string>(_localization[SharedResourcesKey.RoleIsUsed]);

        else if (result == "Success") return Success<string>(_localization[SharedResourcesKey.NotFound]);
        else return BadRequest<string>(result);

    }
}