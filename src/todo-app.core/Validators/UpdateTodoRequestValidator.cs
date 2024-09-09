using FluentValidation;

using TodoApp.Contracts.Requests;
using TodoApp.Core.Repositories;

namespace TodoApp.Core.Validators;

public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
{
    private readonly ITodoRepository _repository;

    public UpdateTodoRequestValidator(ITodoRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Description)
            .NotEmpty()
            .DependentRules(() =>
            {
                RuleFor(x => x.Id)
                    .MustAsync(ValidateTodoAsync)
                    .OverridePropertyName("Todo")
                    .WithMessage("A 'Todo' with a duplicate 'Description' exists in the system");
            });
    }

    private async Task<bool> ValidateTodoAsync(UpdateTodoRequest todo, long id, CancellationToken token = default)
    {
        var existing = await _repository.GetByDescriptionAsync(todo.Description, token);
        if (existing is not null && id != default)
        {
            return existing.Id == todo.Id;
        }

        return existing is null;
    }
}