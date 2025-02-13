using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace TodoApp.Server.OpenApi;

public class TodoAppDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Title = "The todo-app Web API", Version = "v1", Description = "A web API for managing to dos."
        };

        return Task.CompletedTask;
    }
}