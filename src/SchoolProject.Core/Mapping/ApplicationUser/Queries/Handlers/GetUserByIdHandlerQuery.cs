using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Features.Users.Queries.Models;

namespace SchoolProject.Core.Mapping.ApplicationUser.Queries.Handlers;

public class GetUserByIdHandlerQuery : ResponseHandler, IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public GetUserByIdHandlerQuery(IStringLocalizer<SharedResources> localization,
        IMapper mapper, UserManager<User> userManager) : base(localization)
    {
        _localization = localization;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (user is null) return NotFound<GetUserByIdResponse>(_localization[SharedResourcesKey.NotFound]);
        var result = _mapper.Map<GetUserByIdResponse>(user);
        return Success(result);

        //var userById  = await _userManager.FindByIdAsync(request.Id.ToString());
    }
}