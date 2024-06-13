using FluentValidation;

using TodoApp.Core.Models;
using TodoApp.Core.Repositories;

namespace TodoApp.Core.Validators;

public class TodoValidator : AbstractValidator<Todo>
{
    private readonly ITodoRepository _repository;

    public TodoValidator(ITodoRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .MustAsync(ValidateTodoAsync)
            .OverridePropertyName("Todo")
            .WithMessage("A 'Todo' with a duplicate 'Description' exists in the system.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Description)
            .NotEmpty();
    }

    private async Task<bool> ValidateTodoAsync(Todo todo, long id, CancellationToken token = default)
    {
        var existing = await _repository.GetByDescriptionAsync(todo.Description, token);
        if (existing is not null && id != default)
        {
            return existing.Id == todo.Id;
        }

        return existing is null;
    }
}