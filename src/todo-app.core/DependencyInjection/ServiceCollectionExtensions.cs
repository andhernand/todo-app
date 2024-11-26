using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TodoApp.Core.Database;
using TodoApp.Core.Repositories;
using TodoApp.Core.Services;

namespace TodoApp.Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddTodoApiApplication(this IServiceCollection services)
    {
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddValidatorsFromAssemblyContaining<ITodoAppCoreMarker>();
    }

    public static void AddTodoApiDatabase(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("TodoApi")
                               ?? throw new NullReferenceException("TodoApi connection string is null");

        services.AddSingleton<IDbConnectionFactory>(_ => new SqlServerConnectionFactory(connectionString));
    }
}