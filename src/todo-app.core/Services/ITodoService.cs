using TodoApp.Core.Models;

namespace TodoApp.Core.Services;

public interface ITodoService
{
    Task<long?> CreateAsync(Todo todo, CancellationToken token = default);
    Task<Todo?> GetByIdAsync(long id, CancellationToken token = default);
    Task<IEnumerable<Todo>> GetAllAsync(CancellationToken token = default);
    Task<Todo?> UpdateAsync(Todo todo, CancellationToken token = default);
    Task<bool> DeleteAsync(long id, CancellationToken token = default);
}