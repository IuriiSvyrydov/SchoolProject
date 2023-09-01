using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators;

public class EditRoleValidators : AbstractValidator<EditRoleCommand>
{
    #region private fields
    private readonly IStringLocalizer<SharedResources> _localizer;


    #endregion
    public EditRoleValidators(IStringLocalizer<SharedResources> sharedResources)
    {
        _localizer = sharedResources;
        ApplyValidationRules();
        ApplyCustomValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);

    }

    public void ApplyCustomValidationRules()
    {

    }

}