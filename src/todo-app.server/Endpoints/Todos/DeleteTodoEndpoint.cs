using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class DeleteTodoEndpoint
{
    private const string Name = "DeleteTodo";

    public static void MapDeleteTodoEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete(ApiEndpoints.Todos.Delete,
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
            .WithTags(ApiEndpoints.Todos.Tag)
            .WithOpenApi();
    }
}