using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class GetTodoByIdEndpoint
{
    public const string Name = "GetTodoById";

    public static void MapGetTodoByIdEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(ApiEndpoints.Todos.GetById,
                async Task<Results<Ok<TodoResponse>, NotFound>> (
                    long id,
                    ITodoService service,
                    CancellationToken token = default) =>
                {
                    var todo = await service.GetByIdAsync(id, token);
                    if (todo is null)
                    {
                        return TypedResults.NotFound();
                    }

                    return TypedResults.Ok(todo);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .WithOpenApi();
    }
}