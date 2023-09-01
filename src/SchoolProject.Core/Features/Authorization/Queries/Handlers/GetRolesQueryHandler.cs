using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers;

public class GetRolesQueryHandler : ResponseHandler, IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IStringLocalizer<SharedResources> _localization;
    private IMapper _mapper;

    public GetRolesQueryHandler(IStringLocalizer<SharedResources> localization, IAuthorizationService authorizationService, IMapper mapper) : base(localization)
    {
        _localization = localization;
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
    {
        var roles = await _authorizationService.GetRoleListAsync();
        var result = _mapper.Map<List<GetRolesListResult>>(roles);
        return Success(result);
    }
}