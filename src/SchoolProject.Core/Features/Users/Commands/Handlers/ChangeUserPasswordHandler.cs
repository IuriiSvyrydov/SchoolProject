namespace SchoolProject.Core.Features.Users.Commands.Handlers;

public class ChangeUserPasswordHandler : ResponseHandler, IRequestHandler<ChangeUserPasswordCommand, Response<string>>
{
    private readonly IStringLocalizer<SharedResources> _localization;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public ChangeUserPasswordHandler(IStringLocalizer<SharedResources> localization,
        IMapper mapper, UserManager<User> userManager) : base(localization)
    {
        _localization = localization;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());

        if (user is null) return NotFound<string>();
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        //var user1 = await _userManager.HasPasswordAsync(user);
        //await _userManager.RemovePasswordAsync(user);
        //await _userManager.AddPasswordAsync(user, request.NewPassword);
        if (!result.Succeeded) return BadRequest<string>(_localization[SharedResourcesKey.ChangePasswordFailed]);
        return Success((string)_localization[SharedResourcesKey.ChangePasswordSuccessfully]);



    }
}