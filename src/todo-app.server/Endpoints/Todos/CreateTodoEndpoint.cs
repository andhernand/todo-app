using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;
using TodoApp.Server.Endpoints.Todos.Filters;

namespace TodoApp.Server.Endpoints.Todos;

public static class CreateTodoEndpoint
{
    private const string Name = "CreateTodo";

    public static void MapCreateTodoEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(ApiEndpoints.Todos.Create,
                async Task<Results<CreatedAtRoute<TodoResponse>, ValidationProblem>> (
                    CreateTodoRequest request,
                    ITodoService service,
                    CancellationToken token = default) =>
                {
                    var created = await service.CreateAsync(request, token);

                    return TypedResults.CreatedAtRoute(
                        created,
                        GetTodoByIdEndpoint.Name,
                        new { id = created.Id });
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .Accepts<CreateTodoRequest>(contentType: "application/json")
            .AddEndpointFilter<RequestValidationFilter<CreateTodoRequest>>()
            .WithOpenApi();
    }
}