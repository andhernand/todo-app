using Bogus;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;

namespace Todo.App.Tests.Integration;

public static class Mother
{
    public const string TodosBasePath = "/api/v1/todos";

    public static CreateTodoRequest GenerateCreateTodoRequest(
        string? description = null,
        bool? isCompleted = null)
    {
        return new Faker<CreateTodoRequest>()
            .RuleFor(x => x.Description, f => description ?? f.Lorem.Sentence(3))
            .RuleFor(x => x.IsCompleted, f => isCompleted ?? f.Random.Bool())
            .Generate();
    }

    public static UpdateTodoRequest GenerateUpdateTodoRequest(
        long? id = null,
        string? description = null,
        bool? isCompleted = null)
    {
        return new Faker<UpdateTodoRequest>()
            .RuleFor(x => x.Id, f => id ?? f.Random.Long())
            .RuleFor(x => x.Description, f => description ?? f.Lorem.Sentence(3))
            .RuleFor(x => x.IsCompleted, f => isCompleted ?? f.Random.Bool())
            .Generate();
    }

    public static long GeneratePositiveLong(long min = 10_000, long max = long.MaxValue)
    {
        var faker = new Faker();
        return faker.Random.Long(min, max);
    }

    public static async Task<TodoResponse> CreateTodoAsync(
        HttpClient client,
        string? description = null,
        bool? isCompleted = null)
    {
        var request = GenerateCreateTodoRequest(description: description, isCompleted: isCompleted);
        var response = await client.PostAsJsonAsync(TodosBasePath, request);
        var todo = await response.Content.ReadFromJsonAsync<TodoResponse>();
        return todo!;
    }
}