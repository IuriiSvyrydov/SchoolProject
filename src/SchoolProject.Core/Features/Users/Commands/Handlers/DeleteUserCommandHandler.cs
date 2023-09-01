namespace SchoolProject.Core.Features.Users.Commands.Handlers;

public class DeleteUserCommandHandler : ResponseHandler, IRequestHandler<DeleteUserCommand, Response<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private IStringLocalizer<SharedResources> _localization;

    public DeleteUserCommandHandler(IStringLocalizer<SharedResources> localization, IMapper mapper, UserManager<User> userManager) : base(localization)
    {

        _mapper = mapper;
        _userManager = userManager;
        _localization = localization;
    }

    public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) return NotFound<string>();
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return Deleted<string>(_localization[SharedResourcesKey.DeletedFailed]);
        return Deleted<string>(_localization[SharedResourcesKey.Deleted]);

    }
}