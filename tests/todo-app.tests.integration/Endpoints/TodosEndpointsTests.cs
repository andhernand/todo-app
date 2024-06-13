using System.Net;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using TodoApp.Contracts.Responses;

namespace Todo.App.Tests.Integration.Endpoints;

public class TodosEndpointsTests(TodoApiFactory _factory) : IClassFixture<TodoApiFactory>, IAsyncLifetime
{
    private readonly List<long> _createdTodos = [];

    [Fact]
    public async Task CreateTodo_WhenDataIsCorrect_ShouldCreateTodo()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var request = Mother.GenerateCreateTodoRequest();
        var expected = new TodoResponse
        {
            Id = -1, Description = request.Description, IsCompleted = request.IsCompleted
        };

        // Act
        var response = await client.PostAsJsonAsync(Mother.TodosBasePath, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var actual = await response.Content.ReadFromJsonAsync<TodoResponse>();

        response.Headers.Location.Should().Be($"http://localhost{Mother.TodosBasePath}/{actual!.Id}");
        _createdTodos.Add(actual.Id);

        actual.Id.Should().NotBe(default);
        actual.Id.Should().BeGreaterOrEqualTo(1);
        actual.Description.Should().Be(expected.Description);
        actual.IsCompleted.Should().Be(expected.IsCompleted);
    }

    [Fact]
    public async Task CreateTodo_WhenDescriptionIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var request = Mother.GenerateCreateTodoRequest(description: "");
        var expected = Mother.GenerateValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Description", ["'Description' must not be empty."] }
        });

        // Act
        var response = await client.PostAsJsonAsync(Mother.TodosBasePath, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateTodo_WhenTodoWithDescriptionAlreadyExists_ShouldReturnBadRequest()
    {
        // Arrange
        using var client = _factory.CreateClient();

        var todo = await Mother.CreateTodoAsync(client);
        _createdTodos.Add(todo.Id);

        var request = Mother.GenerateCreateTodoRequest(description: todo.Description, isCompleted: todo.IsCompleted);
        var expected = Mother.GenerateValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Todo", ["A 'Todo' with a duplicate 'Description' exists in the system."] }
        });

        // Act
        var response = await client.PostAsJsonAsync(Mother.TodosBasePath, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateTodo_WhenTodoWithDescriptionWithMixedCaseAlreadyExists_ShouldReturnBadRequest()
    {
        // Arrange
        using var client = _factory.CreateClient();

        var todo = await Mother.CreateTodoAsync(client, description: "THIS iS Mixed CasE.");
        _createdTodos.Add(todo.Id);

        var request = Mother.GenerateCreateTodoRequest(description: todo.Description.ToLowerInvariant(),
            isCompleted: todo.IsCompleted);
        var expected = Mother.GenerateValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Todo", ["A 'Todo' with a duplicate 'Description' exists in the system."] }
        });

        // Act
        var response = await client.PostAsJsonAsync(Mother.TodosBasePath, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().BeEquivalentTo(expected);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (var createdTodo in _createdTodos)
        {
            _ = await httpClient.DeleteAsync($"{Mother.TodosBasePath}/{createdTodo}");
        }
    }
}