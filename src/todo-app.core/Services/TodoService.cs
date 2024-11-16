using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Mapping;
using TodoApp.Core.Repositories;

namespace TodoApp.Core.Services;

public class TodoService(ITodoRepository repository) : ITodoService
{
    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request, CancellationToken token = default)
    {
        var todoRequest = request.MapToTodo();
        var todo = await repository.CreateAsync(todoRequest, token);
        return todo.MapToResponse();
    }

    public async Task<TodoResponse?> GetByIdAsync(long id, CancellationToken token = default)
    {
        var todo = await repository.GetByIdAsync(id, token);
        return todo?.MapToResponse();
    }

    public async Task<IEnumerable<TodoResponse>> GetAllAsync(CancellationToken token = default)
    {
        var todos = await repository.GetAllAsync(token);
        return todos.MapToResponse();
    }

    public async Task<TodoResponse?> UpdateAsync(
        long id,
        UpdateTodoRequest request,
        CancellationToken token = default)
    {
        var todo = request.MapToTodo(id);
        var updated = await repository.UpdateAsync(todo, token);
        return updated
            ? todo.MapToResponse()
            : null;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken token = default)
    {
        return await repository.DeleteAsync(id, token);
    }
}