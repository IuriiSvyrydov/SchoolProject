using FluentValidation;

namespace SchoolProject.Core.Features.Users.Commands.Validators;

public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
        ApplyValidationsRules();
        ApplyCustomValidationsRules();
    }

    private void ApplyCustomValidationsRules()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .WithMessage(_localizer[SharedResourcesKey.Required]);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword).WithMessage(_localizer[SharedResourcesKey.PasswordNotEqualsConfirm]);
    }

    private void ApplyValidationsRules()
    {

    }

}