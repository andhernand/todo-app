using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Models;

namespace TodoApp.Core.Services;

public interface ITodoService
{
    Task<TodoResponse> CreateAsync(CreateTodoRequest request, CancellationToken token = default);
    Task<TodoResponse?> GetByIdAsync(long id, CancellationToken token = default);
    Task<IEnumerable<TodoResponse>> GetAllAsync(CancellationToken token = default);
    Task<Todo?> UpdateAsync(Todo todo, CancellationToken token = default);
    Task<bool> DeleteAsync(long id, CancellationToken token = default);
}