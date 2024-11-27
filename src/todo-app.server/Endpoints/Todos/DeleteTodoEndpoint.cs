using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class DeleteTodoEndpoint
{
    private const string Name = "DeleteTodo";
    private const string Description = "Delete a todo";
    private const string Route = "{id:long}";

    public static RouteGroupBuilder MapDeleteTodoEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete(Route,
                async Task<Results<NoContent, NotFound>> (
                    long id,
                    ITodoService service,
                    CancellationToken token = default) =>
                {
                    var deleted = await service.DeleteAsync(id, token);

                    return deleted
                        ? TypedResults.NoContent()
                        : TypedResults.NotFound();
                })
            .WithName(Name)
            .WithDescription(Description)
            .MapToApiVersion(1);

        return group;
    }
}