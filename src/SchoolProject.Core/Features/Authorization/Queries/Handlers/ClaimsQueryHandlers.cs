using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.Requests.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers;

public class ClaimsQueryHandlers : ResponseHandler,
    IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IAuthorizationService _authorizationService;
    private readonly UserManager<User> _userManager;

    public ClaimsQueryHandlers(IStringLocalizer<SharedResources> localization,
        IAuthorizationService authorizationService, UserManager<User> userManager) : base(localization)
    {
        _localization = localization;
        _authorizationService = authorizationService;
        _userManager = userManager;
    }

    public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return NotFound<ManageUserClaimsResult>(_localization[SharedResourcesKey.UserIsNotFound]);
        var result = await _authorizationService.ManageUserClaimData(user);
        return Success(result);


    }
}
