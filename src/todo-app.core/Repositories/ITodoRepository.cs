using TodoApp.Core.Models;

namespace TodoApp.Core.Repositories;

public interface ITodoRepository
{
    Task<Todo> CreateAsync(Todo todo, CancellationToken token = default);
    Task<Todo?> GetByIdAsync(long id, CancellationToken token = default);
    Task<Todo?> GetByDescriptionAsync(string description, CancellationToken token = default);
    Task<IEnumerable<Todo>> GetAllAsync(CancellationToken token = default);
    Task<bool> UpdateAsync(Todo todo, CancellationToken token = default);
    Task<bool> DeleteAsync(long id, CancellationToken token = default);
    Task<bool> ExistsById(long id, CancellationToken token = default);
}