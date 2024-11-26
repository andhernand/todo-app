using System.Net.Mime;

using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class CreateTodoEndpoint
{
    private const string Name = "CreateTodo";
    private const string Description = "Create a new Todo";
    private const string Route = "/";

    public static RouteGroupBuilder MapCreateTodoEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost(Route,
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
            .WithDescription(Description)
            .Accepts<CreateTodoRequest>(false, MediaTypeNames.Application.Json)
            .AddEndpointFilter<RequestValidationFilter<CreateTodoRequest>>();

        return group;
    }
}