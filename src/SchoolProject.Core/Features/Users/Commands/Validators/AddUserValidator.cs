using FluentValidation;

namespace SchoolProject.Core.Features.Users.Commands.Validators;

public class AddUserValidator : AbstractValidator<AddUserCommand>
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    public AddUserValidator(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
        ApplyValidationsRules();
        ApplyCustomValidationsRules();
    }

    private void ApplyCustomValidationsRules()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .WithMessage(_localizer[SharedResourcesKey.Required])
            .NotNull()
            .MaximumLength(200).WithMessage(_localizer[SharedResourcesKey.MaxLength]);
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .WithMessage(_localizer[SharedResourcesKey.Required])
            .NotNull()
            .MaximumLength(2000).WithMessage(_localizer[SharedResourcesKey.MaxLength]);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required]);
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKey.PasswordNotEqualsConfirm]);
    }

    private void ApplyValidationsRules()
    {

    }
}