using TodoApp.Contracts.Requests;
using TodoApp.Contracts.Responses;
using TodoApp.Core.Models;

namespace TodoApp.Core.Mapping;

public static class ContractMapping
{
    public static TodoResponse MapToResponse(this Todo todo)
    {
        return new TodoResponse { Id = todo.Id, Description = todo.Description, IsCompleted = todo.IsCompleted };
    }

    public static TodosResponse MapToResponse(this IEnumerable<Todo> todos)
    {
        return new TodosResponse { Todos = todos.Select(MapToResponse) };
    }

    public static Todo MapToTodo(this CreateTodoRequest request)
    {
        return new Todo { Description = request.Description, IsCompleted = request.IsCompleted };
    }

    public static Todo MapToTodo(this UpdateTodoRequest request, long id)
    {
        return new Todo { Id = id, Description = request.Description, IsCompleted = request.IsCompleted };
    }
}