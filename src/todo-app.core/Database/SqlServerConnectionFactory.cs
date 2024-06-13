using System.Data;

using Microsoft.Data.SqlClient;

namespace TodoApp.Core.Database;

public class SqlServerConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(token);
        return connection;
    }
}