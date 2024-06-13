using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Server.Mapping;

public class ValidationMappingMiddleware(RequestDelegate next)
{
    private readonly Serilog.ILogger _logger = Serilog.Log.ForContext<ValidationMappingMiddleware>();

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var problems = ex.Errors.GroupBy(error => error.PropertyName)
                .ToDictionary(group => group.Key, group => group
                    .Select(error => error.ErrorMessage)
                    .ToArray());
            var validationProblem = new ValidationProblemDetails(problems) { Status = StatusCodes.Status400BadRequest };
            _logger.Warning("Validation failure: {@ValidationProblemDetails}", validationProblem);
            await context.Response.WriteAsJsonAsync(validationProblem);
        }
    }
}