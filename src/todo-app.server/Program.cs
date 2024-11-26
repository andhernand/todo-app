using Serilog;

using TodoApp.Core.DependencyInjection;
using TodoApp.Core.Health;
using TodoApp.Server.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logConfig) =>
    logConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name);

builder.Services.AddTodoApiDatabase(builder.Configuration);
builder.Services.AddTodoApiApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseHealthChecks(new PathString("/_health"));
app.MapTodoApiEndpoints();

app.Run();