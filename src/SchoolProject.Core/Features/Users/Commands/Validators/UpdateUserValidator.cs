using FluentValidation;

namespace SchoolProject.Core.Features.Users.Commands.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    public UpdateUserValidator(IStringLocalizer<SharedResources> localizer)
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

    }

    private void ApplyValidationsRules()
    {

    }
}