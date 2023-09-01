using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers;

public class EditRoleCommandHandler : ResponseHandler, IRequestHandler<EditRoleCommand, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthorizationService _authorizationService;
    public EditRoleCommandHandler(IStringLocalizer<SharedResources> localization,
        IAuthorizationService authorizationService) : base(localization)
    {
        _localization = localization;
        _authorizationService = authorizationService;
    }

    public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.EditRoleAsync(request);
        if (result == "NotFound") return NotFound<string>();
        else if (result == "Success") return Success<string>(_localization[SharedResourcesKey.RoleEdit]);
        else
            return BadRequest<string>(result);




    }
}