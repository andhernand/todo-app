using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using TodoApp.Core.Database;
using TodoApp.Core.Repositories;
using TodoApp.Core.Services;

namespace TodoApp.Core;

public static class ServiceCollectionExtensions
{
    public static void AddTodoAppCore(this IServiceCollection services)
    {
        services.AddSingleton<ITodoService, TodoService>();
        services.AddSingleton<ITodoRepository, TodoRepository>();
        services.AddValidatorsFromAssemblyContaining<ITodoAppCoreMarker>(ServiceLifetime.Singleton);
    }

    public static void AddTodoAppCoreDatabase(this IServiceCollection services, DatabaseOptions options)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new SqlServerConnectionFactory(options.ConnectionString));
    }
}