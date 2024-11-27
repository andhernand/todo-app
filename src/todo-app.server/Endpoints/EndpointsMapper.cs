using Asp.Versioning;

using TodoApp.Server.Endpoints.Todos;

namespace TodoApp.Server.Endpoints;

public static class EndpointsMapper
{
    public static IEndpointRouteBuilder MapTodoApiEndpoints(this IEndpointRouteBuilder builder)
    {
        var apiVersionSet = builder.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        builder.MapGroup("api/v{version:apiVersion}/todos")
            .MapCreateTodoEndpoint()
            .MapGetAllTodosEndpoint()
            .MapGetTodoByIdEndpoint()
            .MapUpdateTodoEndpoint()
            .MapDeleteTodoEndpoint()
            .WithTags("Todos")
            .WithApiVersionSet(apiVersionSet)
            .WithOpenApi();

        return builder;
    }
}