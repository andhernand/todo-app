namespace Todo.App.Contracts.Responses;

public record TodosResponse
{
    public required IEnumerable<TodoResponse> Todos { get; init; } = [];
}