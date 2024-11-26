using System.Net.Mime;

using Microsoft.AspNetCore.Http.HttpResults;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;

namespace TodoApp.Server.Endpoints.Todos;

public static class UpdateTodoEndpoint
{
    private const string Name = "UpdateTodo";
    private const string Description = "Update a Todo";
    private const string Route = "{id:long}";

    public static RouteGroupBuilder MapUpdateTodoEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut(Route,
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
            .WithDescription(Description)
            .Accepts<UpdateTodoRequest>(false, MediaTypeNames.Application.Json)
            .AddEndpointFilter<RequestValidationFilter<UpdateTodoRequest>>();

        return group;
    }
}