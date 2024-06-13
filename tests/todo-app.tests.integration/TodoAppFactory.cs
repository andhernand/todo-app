using System.Data;

using Dapper;

using DotNetEnv;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;

using Testcontainers.MsSql;

using TodoApp.Server;

namespace Todo.App.Tests.Integration;

public class TodoAppFactory : WebApplicationFactory<ITodoAppMarker>, IAsyncLifetime
{
    private static readonly string ApiUserName;
    private static readonly string ApiPassword;
    private static readonly string SaPassword;
    private static readonly string RootFolder;

    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
        .WithPassword(SaPassword)
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithEnvironment("MSSQL_PID", "Developer")
        .Build();

    static TodoAppFactory()
    {
        Env.TraversePath().Load();

        ApiUserName = GetEnvironmentVariableOrThrow(VariableNames.TodoApiPassword);
        ApiPassword = GetEnvironmentVariableOrThrow(VariableNames.TodoApiUserName);
        SaPassword = GetEnvironmentVariableOrThrow(VariableNames.TodoSaPassword);
        RootFolder = DirectoryFinder.GetDirectoryContaining(VariableNames.TodoSolutionFile)
                     ?? throw new DirectoryNotFoundException("Solution folder not found");
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync().ConfigureAwait(false);
        await InitializeDatabaseAsync().ConfigureAwait(false);
        await RunDatabaseMigrationScriptsAsync().ConfigureAwait(false);
    }

    public new async Task DisposeAsync()
    {
        await _sqlContainer.StopAsync().ConfigureAwait(false);
        await _sqlContainer.DisposeAsync().ConfigureAwait(false);
        await base.DisposeAsync().ConfigureAwait(false);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Database:ConnectionString", BuildDatabaseConnectionString());
        base.ConfigureWebHost(builder);
    }

    private async Task InitializeDatabaseAsync()
    {
        var initScriptPath = Path.Combine(RootFolder, "db", "init.sql");

        if (!File.Exists(initScriptPath))
        {
            throw new FileNotFoundException($"{initScriptPath} file not found");
        }

        var initScript = await File.ReadAllTextAsync(initScriptPath).ConfigureAwait(false);
        var script = initScript.Replace("$(varApiPassword)", ApiPassword);
        await _sqlContainer.ExecScriptAsync(script).ConfigureAwait(false);
    }

    private async Task RunDatabaseMigrationScriptsAsync()
    {
        var migrationScripts = LoadMigrationScriptsFromFileSystem();
        await using var connection = new SqlConnection(BuildDatabaseConnectionString("sa", SaPassword));
        await connection.OpenAsync().ConfigureAwait(false);

        foreach (var migrationScript in migrationScripts)
        {
            var script = await File.ReadAllTextAsync(migrationScript).ConfigureAwait(false);
            var migrations = script.Split(["GO"], StringSplitOptions.RemoveEmptyEntries);

            foreach (var migration in migrations)
            {
                await connection.ExecuteAsync(
                        new CommandDefinition(
                            migration,
                            commandType: CommandType.Text))
                    .ConfigureAwait(false);
            }
        }
    }

    private static IOrderedEnumerable<string> LoadMigrationScriptsFromFileSystem()
    {
        var migrationScriptsFolder = Path.Combine(RootFolder, "db", "migrations");

        if (!Directory.Exists(migrationScriptsFolder))
        {
            throw new DirectoryNotFoundException($"Migration scripts folder not found: {migrationScriptsFolder}");
        }

        return Directory.GetFiles(migrationScriptsFolder, "V*.sql").OrderBy(f => f);
    }

    private static class VariableNames
    {
        public const string TodoApiPassword = "TODO_API_USER_PASSWORD";
        public const string TodoApiUserName = "TODO_API_USER_NAME";
        public const string TodoSaPassword = "TODO_API_SA_PASSWORD";
        public const string TodoSolutionFile = "todo-app.sln";
        public const string TodoDatabaseName = "TodoApiDb";
        public const string TodoApplicationName = "TodoApiTests";
    }

    private static string GetEnvironmentVariableOrThrow(string variableName)
    {
        return Environment.GetEnvironmentVariable(variableName)
               ?? throw new InvalidOperationException($"Environment variable not set: {variableName}");
    }

    private string BuildDatabaseConnectionString(string? userName = default, string? password = default)
    {
        return new SqlConnectionStringBuilder
        {
            DataSource = $"{_sqlContainer.Hostname},{_sqlContainer.GetMappedPublicPort(MsSqlBuilder.MsSqlPort)}",
            InitialCatalog = VariableNames.TodoDatabaseName,
            UserID = userName ?? ApiUserName,
            Password = password ?? ApiPassword,
            ApplicationName = VariableNames.TodoApplicationName,
            ApplicationIntent = ApplicationIntent.ReadWrite,
            TrustServerCertificate = true
        }.ConnectionString;
    }
}