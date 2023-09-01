namespace SchoolProject.Core.Features.Users.Commands.Handlers;

public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
{
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localization;

    private readonly IApplicationUserService _userService;
    public UserCommandHandler(IStringLocalizer<SharedResources> localization,
        IMapper mapper, IApplicationUserService userService) : base(localization)
    {
        _localization = localization;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var identityUser = _mapper.Map<User>(request);
        var result = await _userService.AddUserAsync(identityUser, request.Password);
        switch (result)
        {
            case "EmailIsExist": return BadRequest<string>(_localization[SharedResourcesKey.EmailIsExist]);
            case "UserNameIsExist": return BadRequest<string>(_localization[SharedResourcesKey.UserNameIsExist]);
            case "ErrorInCreateUser":
                return BadRequest<string>(_localization[SharedResourcesKey.FailedToAddUser]);
            case "Failed":
                return BadRequest<string>(_localization[SharedResourcesKey.TryToRegisterAgain]);
                return Success<string>("");
            default: return BadRequest<string>(result);

        }


    }
}