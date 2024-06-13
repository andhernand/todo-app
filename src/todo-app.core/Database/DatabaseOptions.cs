namespace TodoApp.Core.Database;

public class DatabaseOptions
{
    public const string Key = "Database";
    public string ConnectionString { get; set; } = string.Empty;
}