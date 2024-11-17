using FluentValidation;

namespace TodoApp.Server.Endpoints.Todos.Filters;

public class RequestValidationFilter<T>(
    IValidator<T> validator,
    ILogger<RequestValidationFilter<T>> logger)
    : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<T>().First();
        var result = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
        if (result.IsValid)
        {
            return await next(context);
        }

        var validationProblem = TypedResults.ValidationProblem(result.ToDictionary());
        logger.LogWarning("Request validation failure {@ValidationProblemDetails}", validationProblem);

        return validationProblem;
    }
}