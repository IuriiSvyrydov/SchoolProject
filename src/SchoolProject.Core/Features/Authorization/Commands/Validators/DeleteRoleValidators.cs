using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators;

public class DeleteRoleValidators : AbstractValidator<DeleteRoleCommand>
{
    #region private fields
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IAuthorizationService _authorizationService;


    #endregion
    public DeleteRoleValidators(IStringLocalizer<SharedResources> sharedResources, IAuthorizationService authorizationService)
    {
        _localizer = sharedResources;
        _authorizationService = authorizationService;
        ApplyValidationRules();
        ApplyCustomValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
    }

    public void ApplyCustomValidationRules()
    {
        //RuleFor(x => x.Id)
        //    .MustAsync(async (key, CancellationToken) =>
        //        await _authorizationService.IsRoleExistById(key))
        //    .WithMessage(_localizer[SharedResourcesKey.RoleNotExist]);

    }

}