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

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x)
            .MustAsync(ValidateTodoAsync)
            .OverridePropertyName("Todo")
            .WithMessage("'Todo' with a duplicate 'Description' already exists in the system.")
            .When(x => x.Id != default);
    }

    private async Task<bool> ValidateTodoAsync(Todo todo, CancellationToken token = default)
    {
        var existing = await _repository.GetByDescriptionAsync(todo.Description, token);
        if (existing is not null)
        {
            return existing.Id == todo.Id;
        }

        return existing is null;
    }
}