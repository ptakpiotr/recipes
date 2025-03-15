using Microsoft.Extensions.Logging;
using Recipes.Domain.Common.Enums;

namespace Recipes.Application.Common.Validators;

public class ValidationPipelineBehaviour<TRequest, TResponse>(IValidator<TRequest> validator, ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, OneOf<SuccessWithValue<TResponse>, Error>> where TRequest : IRequest<TResponse>
{
    public async Task<OneOf<SuccessWithValue<TResponse>, Error>> Handle(TRequest request,
        RequestHandlerDelegate<OneOf<SuccessWithValue<TResponse>, Error>> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResult = await validator.ValidateAsync(context, cancellationToken).ConfigureAwait(ConfigureAwaitOptions.None);

        if (validationResult.IsValid) return await next().ConfigureAwait(ConfigureAwaitOptions.None);

        var errors = string.Join(", ", validationResult.Errors);
        logger.LogError("Validation failed, errors: {Errors}", errors);

        return new Error(ErrorType.Validation, errors);
    }
}