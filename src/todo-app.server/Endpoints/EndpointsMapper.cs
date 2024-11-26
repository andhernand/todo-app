using TodoApp.Server.Endpoints.Todos;

namespace TodoApp.Server.Endpoints;

public static class EndpointsMapper
{
    public static IEndpointRouteBuilder MapTodoApiEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("api/todos")
            .MapCreateTodoEndpoint()
            .MapGetAllTodosEndpoint()
            .MapGetTodoByIdEndpoint()
            .MapUpdateTodoEndpoint()
            .MapDeleteTodoEndpoint()
            .WithTags("Todos")
            .WithOpenApi();

        return builder;
    }
}