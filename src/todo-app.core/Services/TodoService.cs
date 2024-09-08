using FluentValidation;

using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Mapping;
using TodoApp.Core.Models;
using TodoApp.Core.Repositories;

namespace TodoApp.Core.Services;

public class TodoService(ITodoRepository repository, IValidator<Todo> validator) : ITodoService
{
    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request, CancellationToken token = default)
    {
        var todoRequest = request.MapToTodo();

        await validator.ValidateAndThrowAsync(todoRequest, token);

        var todo = await repository.CreateAsync(todoRequest, token);
        return todo.MapToResponse();
    }

    public Task<Todo?> GetByIdAsync(long id, CancellationToken token = default)
    {
        return repository.GetByIdAsync(id, token);
    }

    public Task<IEnumerable<Todo>> GetAllAsync(CancellationToken token = default)
    {
        return repository.GetAllAsync(token);
    }

    public async Task<Todo?> UpdateAsync(Todo todo, CancellationToken token = default)
    {
        await validator.ValidateAndThrowAsync(todo, token);
        var exists = await repository.ExistsById(todo.Id, token);
        if (!exists)
        {
            return null;
        }

        _ = await repository.UpdateAsync(todo, token);
        return todo;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken token = default)
    {
        var exists = await repository.ExistsById(id, token);
        if (!exists)
        {
            return false;
        }

        return await repository.DeleteAsync(id, token);
    }
}