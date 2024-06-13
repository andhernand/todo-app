using Microsoft.AspNetCore.Mvc;

using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;
using TodoApp.Server.Mapping;

namespace TodoApp.Server.Endpoints.Todos;

public static class GetTodoByIdEndpoint
{
    public const string Name = "GetTodoById";

    public static void MapGetTodoByIdEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(ApiEndpoints.Todos.GetById, async (
                long id,
                ITodoService service,
                CancellationToken token = default) =>
            {
                var todo = await service.GetByIdAsync(id, token);
                if (todo is null)
                {
                    return Results.Problem(statusCode: StatusCodes.Status404NotFound);
                }

                var response = todo.MapToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .Produces<TodoResponse>(contentType: "application/json")
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
    }
}