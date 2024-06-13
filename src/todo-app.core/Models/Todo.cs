namespace TodoApp.Core.Models;

public record Todo
{
    public long Id { get; set; }
    public required string Description { get; init; }
    public required bool IsCompleted { get; init; }
}