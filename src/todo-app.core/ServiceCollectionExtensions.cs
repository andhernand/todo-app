using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using TodoApp.Core.Database;
using TodoApp.Core.Repositories;
using TodoApp.Core.Services;

namespace TodoApp.Core;

public static class ServiceCollectionExtensions
{
    public static void AddTodoApiApplication(this IServiceCollection services)
    {
        services.AddSingleton<ITodoRepository, TodoRepository>();
        services.AddTransient<ITodoService, TodoService>();

        services.AddValidatorsFromAssemblyContaining<ITodoAppCoreMarker>(ServiceLifetime.Singleton);
    }

    public static void AddTodoApiDatabase(this IServiceCollection services, DatabaseOptions dbOptions)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new SqlServerConnectionFactory(dbOptions.ConnectionString));
    }
}