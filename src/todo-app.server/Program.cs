using Serilog;

using TodoApp.Core.DependencyInjection;
using TodoApp.Core.Health;
using TodoApp.Server.DependencyInjection;
using TodoApp.Server.Endpoints;
using TodoApp.Server.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logConfig) =>
    logConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name);

builder.Services.AddTodoApiDatabase(builder.Configuration);
builder.Services.AddTodoApiApplication();
builder.Services.AddTodoApiVersioning();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"Client.Dashboard.WebApi.{description.GroupName}");
        }
    });
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseHealthChecks(new PathString("/_health"));
app.MapTodoApiEndpoints();

app.Run();