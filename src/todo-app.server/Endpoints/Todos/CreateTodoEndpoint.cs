using Microsoft.AspNetCore.Mvc;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;
using TodoApp.Server.Mapping;

namespace TodoApp.Server.Endpoints.Todos;

public static class CreateTodoEndpoint
{
    private const string Name = "CreateTodo";

    public static void MapCreateTodoEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(ApiEndpoints.Todos.Create, async (
                CreateTodoRequest request,
                ITodoService service,
                CancellationToken token = default) =>
            {
                var todo = request.MapToTodo();
                var createdId = await service.CreateAsync(todo, token);
                todo.Id = createdId!.Value;

                var response = todo.MapToResponse();
                return TypedResults.CreatedAtRoute(
                    response,
                    GetTodoByIdEndpoint.Name,
                    new { id = todo.Id });
            })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .Accepts<CreateTodoRequest>(contentType: "application/json")
            .Produces<TodoResponse>(statusCode: StatusCodes.Status201Created)
            .Produces<ValidationProblemDetails>(
                StatusCodes.Status400BadRequest,
                contentType: "application/problem+json");
    }
}