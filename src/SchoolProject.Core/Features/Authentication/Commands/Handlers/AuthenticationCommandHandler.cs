using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers;

public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<JwtAuthResult>>
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationCommandHandler(IStringLocalizer<SharedResources> localizer,
        UserManager<User> userManager, SignInManager<User> signInManager,
        IAuthenticationService authenticationService) : base(localizer)
    {
        _localizer = localizer;
        _userManager = userManager;
        _signInManager = signInManager;
        _authenticationService = authenticationService;
    }

    public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null) return BadRequest<JwtAuthResult>(_localizer[SharedResourcesKey.UserNameIsNotExist]);
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>(_localizer[SharedResourcesKey.UserNameIsExist]);

        // confirm email

        if (!user.EmailConfirmed)
            return BadRequest<JwtAuthResult>(_localizer[SharedResourcesKey.EmailNotConfirmed]);


        // if (!signInResult.IsCompletedSuccessfully) return BadRequest<JwtAuthResult>(_localizer[SharedResourcesKey.PasswordIsNotCorrect]);
        var result = await _authenticationService.GenerateToken(user);
        return Success(result);

    }
}