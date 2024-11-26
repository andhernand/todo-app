using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class GetAllTodosEndpoint
{
    private const string Name = "GetAllTodos";
    private const string Description = "Get all the todos";
    private const string Route = "/";

    public static RouteGroupBuilder MapGetAllTodosEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet(Route,
                async Task<Ok<IEnumerable<TodoResponse>>> (
                    ITodoService service,
                    CancellationToken token = default) =>
                {
                    var todos = await service.GetAllAsync(token);
                    return TypedResults.Ok(todos);
                })
            .WithName(Name)
            .WithDescription(Description);

        return group;
    }
}