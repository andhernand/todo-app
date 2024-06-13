using TodoApp.Core;
using TodoApp.Core.Database;
using TodoApp.Server.Health;

var builder = WebApplication.CreateBuilder(args);
using var config = builder.Configuration;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name);

builder.Services.AddTodoApiApplication();
builder.Services.AddTodoApiDatabase(
    builder.Configuration.GetSection(DatabaseOptions.Key)
        .Get<DatabaseOptions>()!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHealthChecks(new PathString("/_health"));

app.Run();