using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers;

public class GetRoleByIdQueryHandler : ResponseHandler, IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localization;

    public GetRoleByIdQueryHandler(IStringLocalizer<SharedResources> localization, IAuthorizationService authorizationService, IMapper mapper) : base(localization)
    {
        _localization = localization;
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _authorizationService.GetRoleById(request.Id);
        if (role == null) return NotFound<GetRoleByIdResult>(_localization[SharedResourcesKey.RoleNotExist]);
        var result = _mapper.Map<GetRoleByIdResult>(role);
        return Success(result);
    }
}