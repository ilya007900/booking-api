using Booking.Application.Common;
using FluentValidation;
using MediatR;

namespace Booking.Infrastructure.Pipelines;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures =
            _validators.Select(validator => validator.Validate(context));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.ErrorMessage,
                validationFailure.PropertyName))
            .ToArray();

        if (errors.Any())
        {
            throw new Application.Common.ValidationException(errors);
        }

        var response = await next();

        return response;
    }
}