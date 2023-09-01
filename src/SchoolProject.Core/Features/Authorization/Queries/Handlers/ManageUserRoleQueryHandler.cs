using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.Requests.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers;

public class ManageUserRoleQueryHandler : ResponseHandler, IRequestHandler<ManageUserRoleQuery, Response<ManageUserRoleResult>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public ManageUserRoleQueryHandler(IStringLocalizer<SharedResources> localization, IAuthorizationService authorizationService,
        IMapper mapper, UserManager<User> userManager) : base(localization)
    {
        _localization = localization;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Response<ManageUserRoleResult>> Handle(ManageUserRoleQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) return NotFound<ManageUserRoleResult>(_localization[SharedResourcesKey.UserIsNotFound]);
        var result = await _authorizationService.ManageUserRolesData(user);
        return Success(result);

    }
}