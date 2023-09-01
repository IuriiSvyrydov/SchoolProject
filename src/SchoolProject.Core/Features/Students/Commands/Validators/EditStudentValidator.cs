
using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;

namespace SchoolProject.Core.Features.Students.Commands.Validators;

public class EditStudentValidator : AbstractValidator<EditStudentCommand>
{
    private readonly IStudentService _studentService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    public EditStudentValidator(IStudentService studentService,
        IStringLocalizer<SharedResources> localizer)
    {
        _studentService = studentService;
        _localizer = localizer;
        ApplyValidationRules();
        ApplyCustomValidationRules();
    }
    public void ApplyValidationRules()
    {
        RuleFor(x => x.NameUa)
            .NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .WithMessage(_localizer[SharedResourcesKey.Required])
            .NotNull()
            .MaximumLength(10).WithMessage(_localizer[SharedResourcesKey.MaxLength]);
        RuleFor(x => x.NameUs)
                .NotEmpty()
                .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
                .WithMessage(_localizer[SharedResourcesKey.Required])
                .NotNull()
                .MaximumLength(10).WithMessage(_localizer[SharedResourcesKey.MaxLength]);

        RuleFor(x => x.Address).NotEmpty()
            .WithMessage(_localizer[SharedResourcesKey.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKey.Required])
            .MaximumLength(10).WithMessage(_localizer[SharedResourcesKey.MaxLength]);
    }
    public void ApplyCustomValidationRules()
    {
        RuleFor(x => x.NameUa)
            .MustAsync(async (key, CancellationToken) =>
                !await _studentService.IsNameExist(key))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
        RuleFor(x => x.NameUs)
            .MustAsync(async (key, CancellationToken) =>
                !await _studentService.IsNameExist(key))
            .WithMessage(_localizer[SharedResourcesKey.IsExist]);
    }
}
