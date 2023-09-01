using FluentValidation;
using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Core.Features.Authentication.Queries.Validators;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
{
    private readonly IStringLocalizer<SharedResources> _localizer;

    public ConfirmEmailValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
        ApplyCustomValidationsRules();
        ApplyValidationsRules();
    }

    public void ApplyCustomValidationsRules()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .WithMessage(_localizer[SharedResourcesKey.Required]);
    }

    private void ApplyValidationsRules()
    {

    }
}