namespace TodoApp.Contracts.Requests;

public record UpdateTodoRequest
{
    public required long Id { get; init; }
    public required string Description { get; init; }
    public required bool IsCompleted { get; init; }
}