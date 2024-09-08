using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class GetAllTodosEndpoint
{
    private const string Name = "GetAllTodos";

    public static void MapGetAllTodosEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(ApiEndpoints.Todos.GetAll,
                async Task<Ok<IEnumerable<TodoResponse>>> (
                    ITodoService service,
                    CancellationToken token = default) =>
                {
                    var todos = await service.GetAllAsync(token);
                    return TypedResults.Ok(todos);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .WithOpenApi();
    }
}