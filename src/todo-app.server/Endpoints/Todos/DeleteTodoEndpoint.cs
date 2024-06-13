using Microsoft.AspNetCore.Mvc;

using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class DeleteTodoEndpoint
{
    private const string Name = "DeleteTodo";

    public static void MapDeleteTodoEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete(ApiEndpoints.Todos.Delete, async (
                long id,
                ITodoService service,
                CancellationToken token = default) =>
            {
                var deleted = await service.DeleteAsync(id, token);
                return deleted ? Results.NoContent() : Results.Problem(statusCode: StatusCodes.Status404NotFound);
            })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
    }
}