namespace SchoolProject.Core.Mapping.ApplicationUser.Queries.Handlers;

public class UserQueryHandler : ResponseHandler, IRequestHandler<GetUserPaginationQuery, PaginationResult<GetUserPaginationResponse>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public UserQueryHandler(IStringLocalizer<SharedResources> localization, IMapper mapper,
        UserManager<User> userManager) : base(localization)
    {
        _localization = localization;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<PaginationResult<GetUserPaginationResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users.AsQueryable();
        var paginatedList = await _mapper.ProjectTo<GetUserPaginationResponse>(users)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        return paginatedList;
    }
}

