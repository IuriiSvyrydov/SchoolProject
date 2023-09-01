using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators;

public class AddRoleValidators : AbstractValidator<AddRoleCommand>
{
    #region private fields
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IAuthorizationService _authorizationService;

    #endregion
    public AddRoleValidators(IStringLocalizer<SharedResources> sharedResources, IAuthorizationService authorizationService)
    {
        _localizer = sharedResources;
        _authorizationService = authorizationService;
        ApplyValidationRules();
        ApplyCustomValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);

    }

    public void ApplyCustomValidationRules()
    {
        RuleFor(x => x.RoleName)
            .MustAsync(async (key, CancellationToken) =>
                !await _authorizationService.IsRoleExistByName(key))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
    }
}