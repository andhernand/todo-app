using FluentValidation;

namespace TodoApp.Server.Endpoints.Todos.Filters;

public class RequestValidationFilter<T>(
    IValidator<T> validator,
    Serilog.ILogger logger) : IEndpointFilter
{
    private readonly Serilog.ILogger _logger = logger.ForContext<RequestValidationFilter<T>>();

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<T>().First();
        var result = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
        if (result.IsValid)
        {
            return await next(context);
        }

        var validationProblem = TypedResults.ValidationProblem(result.ToDictionary());
        _logger.Warning("Request validation failure {@ValidationProblemDetails}", validationProblem);

        return validationProblem;
    }
}