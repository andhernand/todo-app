using Scalar.AspNetCore;

using Serilog;

using TodoApp.Core.DependencyInjection;
using TodoApp.Core.Health;
using TodoApp.Server.DependencyInjection;
using TodoApp.Server.Endpoints;
using TodoApp.Server.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logConfig) =>
    logConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddOpenApi("v1", opts =>
{
    opts.AddDocumentTransformer<TodoAppDocumentTransformer>();
    opts.ShouldInclude = description => description.GroupName is "v1";
});

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name);

builder.Services.AddTodoApiDatabase(builder.Configuration);
builder.Services.AddTodoApiApplication();
builder.Services.AddTodoApiVersioning();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseHealthChecks(new PathString("/_health"));
app.MapTodoApiEndpoints();

app.Run();