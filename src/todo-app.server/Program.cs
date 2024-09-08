using Serilog;

using TodoApp.Core;
using TodoApp.Core.Database;
using TodoApp.Server;
using TodoApp.Server.Endpoints;
using TodoApp.Server.Health;
using TodoApp.Server.Mapping;

Log.Logger = Extensions.CreateBootstrapLogger();

try
{
    Log.Information("Starting Todo-App API");

    var builder = WebApplication.CreateBuilder(args);
    {
        builder.Host.UseSerilog((context, logConfig) =>
            logConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name);

        using var config = builder.Configuration;
        var dbOptions = new DatabaseOptions();
        config.GetSection(DatabaseOptions.Key).Bind(dbOptions);

        builder.Services.AddTodoApiDatabase(dbOptions);
        builder.Services.AddTodoApiApplication();
    }

    var app = builder.Build();
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();
        app.UseHealthChecks(new PathString("/_health"));
        app.UseMiddleware<ValidationMappingMiddleware>();
        app.MapApiEndpoints();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}