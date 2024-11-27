using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class GetTodoByIdEndpoint
{
    public const string Name = "GetTodoById";
    private const string Description = "Get a Todo by Id";
    private const string Route = "{id:long}";

    public static RouteGroupBuilder MapGetTodoByIdEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet(Route,
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
            .WithDescription(Description)
            .MapToApiVersion(1);

        return group;
    }
}