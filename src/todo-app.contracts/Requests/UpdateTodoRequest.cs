namespace TodoApp.Contracts.Requests;

public record UpdateTodoRequest
{
    public required string Description { get; init; }
    public required bool IsCompleted { get; init; }
}