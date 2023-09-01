using FluentValidation;
using SchoolProject.Core.Features.Authentication.Queries.Models;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators;

public class ResetPasswordQueryValidator : AbstractValidator<ResetPasswordQuery>
{
    private readonly IStringLocalizer<SharedResources> _localizer;

    public ResetPasswordQueryValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
        ApplyValidationRules();
        ApplyCustomValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
    }

    public void ApplyCustomValidationRules()
    {

    }
}