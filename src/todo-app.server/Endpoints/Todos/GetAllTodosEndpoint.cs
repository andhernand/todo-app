using TodoApp.Contracts.Responses;
using TodoApp.Core.Services;
using TodoApp.Server.Mapping;

namespace TodoApp.Server.Endpoints.Todos;

public static class GetAllTodosEndpoint
{
    private const string Name = "GetAllTodos";

    public static void MapGetAllTodosEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(ApiEndpoints.Todos.GetAll, async (
                ITodoService service,
                CancellationToken token = default) =>
            {
                var todos = await service.GetAllAsync(token);
                var response = todos.MapToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .WithTags(ApiEndpoints.Todos.Tag)
            .Produces<TodosResponse>(contentType: "application/json");
    }
}