using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers;

public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IAuthorizationService _authorizationService;

    public RoleCommandHandler(IStringLocalizer<SharedResources> localization, IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService) : base(localization)
    {
        _localizer = localizer;
        _authorizationService = authorizationService;
    }

    public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.AddRoleAsync(request.RoleName);
        if (result == "Success")
            return Success("");
        return BadRequest<string>(_localizer[SharedResourcesKey.AddFailed]);


    }
}