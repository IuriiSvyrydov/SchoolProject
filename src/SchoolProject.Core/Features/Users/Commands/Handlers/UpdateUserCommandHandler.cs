using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Core.Features.Users.Commands.Handlers;

public class UpdateUserCommandHandler : ResponseHandler, IRequestHandler<UpdateUserCommand, Response<string>>
{
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly UserManager<User> _userManager;
    public UpdateUserCommandHandler(IStringLocalizer<SharedResources> localization,
        IMapper mapper, UserManager<User> userManager) : base(localization)
    {
        _localization = localization;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) return NotFound<string>();
        var newUser = _mapper.Map(request, user);
        var userByName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Id == newUser.Id);
        if (userByName is not null) return BadRequest<string>(_localization[SharedResourcesKey.UserNameIsExist]);
        var result = await _userManager.UpdateAsync(newUser);
        if (!result.Succeeded) return BadRequest<string>(_localization[SharedResourcesKey.UpdatedFailed]);
        return Success("");
    }
}