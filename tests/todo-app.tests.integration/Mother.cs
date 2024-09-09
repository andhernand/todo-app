using Bogus;

using Microsoft.AspNetCore.Mvc;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;

namespace Todo.App.Tests.Integration;

public static class Mother
{
    public const string TodosBasePath = "/api/todos";
    private static readonly Faker Fake = new();

    public static CreateTodoRequest GenerateCreateTodoRequest(
        string? description = default,
        bool? isCompleted = default)
    {
        return new Faker<CreateTodoRequest>()
            .RuleFor(x => x.Description, f => description ?? f.Lorem.Sentence())
            .RuleFor(x => x.IsCompleted, f => isCompleted ?? f.Random.Bool())
            .Generate();
    }

    public static UpdateTodoRequest GenerateUpdateTodoRequest(
        long? id = default,
        string? description = default,
        bool? isCompleted = default)
    {
        return new Faker<UpdateTodoRequest>()
            .RuleFor(x => x.Id, f => id ?? f.Random.Long())
            .RuleFor(x => x.Description, f => description ?? f.Lorem.Sentence())
            .RuleFor(x => x.IsCompleted, f => isCompleted ?? f.Random.Bool())
            .Generate();
    }

    public static long GeneratePositiveLong(long min = 1, long max = long.MaxValue)
    {
        return Fake.Random.Long(min, max);
    }

    public static ValidationProblemDetails GenerateValidationProblemDetails(Dictionary<string, string[]> errors)
    {
        return new ValidationProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Status = StatusCodes.Status400BadRequest,
            Errors = errors
        };
    }

    public static async Task<TodoResponse> CreateTodoAsync(
        HttpClient client,
        string? description = default,
        bool? isCompleted = default)
    {
        var request = GenerateCreateTodoRequest(description: description, isCompleted: isCompleted);
        var response = await client.PostAsJsonAsync(TodosBasePath, request);
        var todo = await response.Content.ReadFromJsonAsync<TodoResponse>();
        return todo!;
    }
}