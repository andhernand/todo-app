namespace TodoApp.Server.Endpoints.Todos;

public static class TodosEndpointsExtensions
{
    public static void MapTodosEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapCreateTodoEndpoint();
        builder.MapGetTodoByIdEndpoint();
    }
}