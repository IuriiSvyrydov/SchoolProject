
using FluentValidation;

namespace SchoolProject.Core.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private IStringLocalizer<SharedResources> _localization;
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<SharedResources> localization)
    {
        _validators = validators;
        _localization = localization;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context,
                cancellationToken)));
            var failures = validationResult.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Count != 0)
            {
                var message = failures.Select(x => _localization[x.PropertyName] + ": " + _localization[x.ErrorMessage])
                    .FirstOrDefault();
                throw new ValidationException(message);
            }
        }
        return await next();
    }
}
