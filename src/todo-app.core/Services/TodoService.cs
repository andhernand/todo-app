using FluentValidation;

using TodoApp.Core.Models;
using TodoApp.Core.Repositories;

namespace TodoApp.Core.Services;

public class TodoService(ITodoRepository _repository, IValidator<Todo> _validator) : ITodoService
{
    public async Task<long?> CreateAsync(Todo todo, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(todo, token);
        return await _repository.CreateAsync(todo, token);
    }

    public Task<Todo?> GetByIdAsync(long id, CancellationToken token = default)
    {
        return _repository.GetByIdAsync(id, token);
    }

    public Task<IEnumerable<Todo>> GetAllAsync(CancellationToken token = default)
    {
        return _repository.GetAllAsync(token);
    }

    public async Task<Todo?> UpdateAsync(Todo todo, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(todo, token);
        var exists = await _repository.ExistsById(todo.Id, token);
        if (!exists)
        {
            return null;
        }

        _ = await _repository.UpdateAsync(todo, token);
        return todo;
    }

    public Task<bool> DeleteAsync(long id, CancellationToken token = default)
    {
        return _repository.DeleteAsync(id, token);
    }
}