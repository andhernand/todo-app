using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class UpdateTodoEndpoint
{
    private const string Name = "UpdateTodo";

    public static void MapUpdateTodoEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut(ApiEndpoints.Todos.Update,
                async Task<Results<Ok<TodoResponse>, NotFound, ValidationProblem>> (
                    long id,
                    UpdateTodoRequest request,
                    ITodoService service,
                    CancellationToken token = default) =>
                {
                    var updatedTodo = await service.UpdateAsync(id, request, token);

                    return updatedTodo is null
                        ? TypedResults.NotFound()
                        : TypedResults.Ok(updatedTodo);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .Accepts<UpdateTodoRequest>(isOptional: false, contentType: "application/json")
            .WithOpenApi();
    }
}