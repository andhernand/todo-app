using System.Data;

using Dapper;

using TodoApp.Core.Database;
using TodoApp.Core.Models;

namespace TodoApp.Core.Repositories;

public class TodoRepository(IDbConnectionFactory _dbConnectionFactory) : ITodoRepository
{
    private readonly Serilog.ILogger _logger = Serilog.Log.ForContext<TodoRepository>();

    public async Task<long?> CreateAsync(Todo todo, CancellationToken token = default)
    {
        _logger.Information("Creating new {@Todo}", todo);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var parameters = new DynamicParameters();
        parameters.Add("@Description", todo.Description, DbType.String, ParameterDirection.Input, 512);
        parameters.Add("@IsCompleted", todo.IsCompleted, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("@TodoId", 0, DbType.Int64, ParameterDirection.Output);

        _ = await connection.ExecuteAsync(new CommandDefinition(
            "[dbo].[usp_Todo_Insert]",
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: token));

        var id = parameters.Get<long>("@TodoId");
        return id;
    }

    public async Task<Todo?> GetByIdAsync(long id, CancellationToken token = default)
    {
        _logger.Information("Get Todo By {Id}", id);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int64, ParameterDirection.Input);

        var todo = await connection.QuerySingleOrDefaultAsync<Todo>(
            new CommandDefinition(
                "[dbo].[usp_Todo_GetById]",
                parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: token));

        return todo;
    }

    public async Task<Todo?> GetByDescriptionAsync(string description, CancellationToken token = default)
    {
        _logger.Information("Get Todo By {Description}", description);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var parameters = new DynamicParameters();
        parameters.Add("@Description", description, DbType.String, ParameterDirection.Input, 512);

        var todo = await connection.QuerySingleOrDefaultAsync<Todo>(
            new CommandDefinition(
                "[dbo].[usp_Todo_GetByDescription]",
                parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: token));

        return todo;
    }

    public async Task<IEnumerable<Todo>> GetAllAsync(CancellationToken token = default)
    {
        _logger.Information("Get All Todos");
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var todo = await connection.QueryAsync<Todo>(
            new CommandDefinition(
                "[dbo].[usp_Todo_GetAll]",
                commandType: CommandType.StoredProcedure,
                cancellationToken: token));

        return todo;
    }

    public async Task<bool> UpdateAsync(Todo todo, CancellationToken token = default)
    {
        _logger.Information("Updating {@Todo}", todo);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var parameters = new DynamicParameters();
        parameters.Add("@Id", todo.Id, DbType.Int64, ParameterDirection.Input);
        parameters.Add("@Description", todo.Description, DbType.String, ParameterDirection.Input, 512);
        parameters.Add("@IsCompleted", todo.IsCompleted, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("@RowCount", 0, DbType.Int32, ParameterDirection.Output);

        _ = await connection.ExecuteAsync(new CommandDefinition(
            "[dbo].[usp_Todo_Update]",
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: token));

        var rowCount = parameters.Get<int>("@RowCount");
        return rowCount > 0;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken token = default)
    {
        _logger.Information("Delete Todo By {Id}", id);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int64, ParameterDirection.Input);
        parameters.Add("@RowCount", 0, DbType.Int32, ParameterDirection.Output);

        _ = await connection.ExecuteAsync(new CommandDefinition(
            "[dbo].[usp_Todo_Delete]",
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: token));

        var rowCount = parameters.Get<int>("@RowCount");
        return rowCount > 0;
    }

    public async Task<bool> ExistsById(long id, CancellationToken token = default)
    {
        _logger.Information("Does Todo Exist with {Id}", id);
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int64, ParameterDirection.Input);

        var exists = await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                "[dbo].[usp_Todo_ExistsById]",
                parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: token));

        return exists;
    }
}