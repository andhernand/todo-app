namespace Todo.App.Contracts.Responses;

public record TodoResponse
{
    public required long Id { get; init; }
    public required string Description { get; init; }
    public required bool IsCompleted { get; init; }
}