﻿using System.Net;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using TodoApp.Contracts.Responses;

namespace Todo.App.Tests.Integration.Endpoints;

public class TodosEndpointsTests(TodoApiFactory factory) : IClassFixture<TodoApiFactory>, IAsyncLifetime
{
    private readonly List<long> _createdTodos = [];

    [Fact]
    public async Task HealthCheck_WhenHealthy_ReturnsHealthy()
    {
        // Arrange
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/_health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsStringAsync();
        message.Should().Be("Healthy");
    }

    [Fact]
    public async Task CreateTodo_WhenDataIsCorrect_ShouldCreateTodo()
    {
        // Arrange
        using var client = factory.CreateClient();
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
        using var client = factory.CreateClient();
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
    public async Task GetTodoById_WhenTodoExists_ShouldReturnTodo()
    {
        // Arrange
        using var client = factory.CreateClient();
        var expected = await Mother.CreateTodoAsync(client);
        _createdTodos.Add(expected.Id);

        // Act
        var response = await client.GetAsync($"{Mother.TodosBasePath}/{expected.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var actual = await response.Content.ReadFromJsonAsync<TodoResponse>();
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetTodoById_WhenTodoDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        using var client = factory.CreateClient();
        var badId = Mother.GeneratePositiveLong();

        // Act
        var response = await client.GetAsync($"{Mother.TodosBasePath}/{badId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAllTodos_WhenTodosExist_ShouldReturnTodos()
    {
        // Arrange
        using var client = factory.CreateClient();

        var todo1 = await Mother.CreateTodoAsync(client);
        var todo2 = await Mother.CreateTodoAsync(client);
        var todo3 = await Mother.CreateTodoAsync(client);
        _createdTodos.Add(todo1.Id);
        _createdTodos.Add(todo2.Id);
        _createdTodos.Add(todo3.Id);

        var expected = new[] { todo1, todo2, todo3 };

        // Act
        var response = await client.GetAsync(Mother.TodosBasePath);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var actual = await response.Content.ReadFromJsonAsync<IEnumerable<TodoResponse>>();
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetAllTodos_WhenTodosDoNotExist_ShouldReturnNoTodos()
    {
        // Arrange
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync(Mother.TodosBasePath);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var actual = await response.Content.ReadFromJsonAsync<IEnumerable<TodoResponse>>();
        actual.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateTodo_WhenDataIsCorrect_ShouldUpdateTodo()
    {
        // Arrange
        using var client = factory.CreateClient();

        var todo = await Mother.CreateTodoAsync(client);
        _createdTodos.Add(todo.Id);

        var request = Mother.GenerateUpdateTodoRequest(todo.Id, todo.Description, !todo.IsCompleted);
        var expected = new TodoResponse
        {
            Id = todo.Id, Description = request.Description, IsCompleted = request.IsCompleted
        };

        // Act
        var response = await client.PutAsJsonAsync($"{Mother.TodosBasePath}/{todo.Id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var actual = await response.Content.ReadFromJsonAsync<TodoResponse>();
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UpdateTodo_WhenIdIsNegative_ShouldReturnBadRequest()
    {
        // Arrange
        using var client = factory.CreateClient();
        var request = Mother.GenerateUpdateTodoRequest(-2);
        var expected = Mother.GenerateValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Id", ["'Id' must be greater than or equal to '1'."] }
        });

        // Act
        var response = await client.PutAsJsonAsync($"{Mother.TodosBasePath}/{-2}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UpdateTodo_WhenDescriptionIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        using var client = factory.CreateClient();

        var todo = await Mother.CreateTodoAsync(client);
        _createdTodos.Add(todo.Id);

        var request = Mother.GenerateUpdateTodoRequest(todo.Id, "", todo.IsCompleted);
        var expected = Mother.GenerateValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Description", ["'Description' must not be empty."] }
        });

        // Act
        var response = await client.PutAsJsonAsync($"{Mother.TodosBasePath}/{todo.Id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UpdateTodo_WhenTodoDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        using var client = factory.CreateClient();
        var badId = Mother.GeneratePositiveLong();
        var request = Mother.GenerateUpdateTodoRequest(badId);

        // Act
        var response = await client.PutAsJsonAsync($"{Mother.TodosBasePath}/{badId}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteTodo_WhenTodoIsDeleted_ShouldReturnNoContent()
    {
        // Arrange
        using var client = factory.CreateClient();
        var createdTodo = await Mother.CreateTodoAsync(client);
        _createdTodos.Add(createdTodo.Id);

        // Act
        var response = await client.DeleteAsync($"{Mother.TodosBasePath}/{createdTodo.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteTodo_WhenTodoDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        using var client = factory.CreateClient();
        var badId = Mother.GeneratePositiveLong();

        // Act
        var response = await client.DeleteAsync($"{Mother.TodosBasePath}/{badId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Swagger_WhenCalled_ShouldReturnOk()
    {
        // Arrange
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/swagger/index.html");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Swagger_WhenJsonIsCalled_ShouldReturnOk()
    {
        // Arrange
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/swagger/v1/swagger.json");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        var httpClient = factory.CreateClient();
        foreach (var createdTodo in _createdTodos)
        {
            _ = await httpClient.DeleteAsync($"{Mother.TodosBasePath}/{createdTodo}");
        }
    }
}