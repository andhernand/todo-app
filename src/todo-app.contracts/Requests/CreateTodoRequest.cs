namespace TodoApp.Contracts.Requests;

public record CreateTodoRequest
{
    public required string Description { get; init; }
    public required bool IsCompleted { get; init; }
}