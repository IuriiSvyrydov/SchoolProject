using FluentValidation;
using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Core.Features.Emails.Commands.Validators;

public class SendEmailValidator : AbstractValidator<SendEmailCommand>
{

    private readonly IStringLocalizer<SharedResources> _localizer;
    public SendEmailValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
        ApplyValidationsRules();
        ApplyCustomValidationsRules();
    }

    public void ApplyCustomValidationsRules()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .WithMessage(_localizer[SharedResourcesKey.Required]);
    }

    private void ApplyValidationsRules()
    {

    }


}