using Microsoft.AspNetCore.Mvc;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Mapping;
using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class UpdateTodoEndpoint
{
    private const string Name = "UpdateTodo";

    public static void MapUpdateTodoEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut(ApiEndpoints.Todos.Update, async (
                long id,
                UpdateTodoRequest request,
                ITodoService service,
                CancellationToken token = default) =>
            {
                var todo = request.MapToTodo(id);
                var updatedTodo = await service.UpdateAsync(todo, token);
                if (updatedTodo is null)
                {
                    return Results.Problem(statusCode: StatusCodes.Status404NotFound);
                }

                var response = updatedTodo.MapToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .Accepts<UpdateTodoRequest>(isOptional: false, contentType: "application/json")
            .Produces<TodoResponse>(contentType: "application/json")
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json")
            .Produces<ValidationProblemDetails>(
                StatusCodes.Status400BadRequest,
                contentType: "application/problem+json");
    }
}