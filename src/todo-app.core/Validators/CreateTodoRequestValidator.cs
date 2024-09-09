using FluentValidation;

using TodoApp.Contracts.Requests;
using TodoApp.Core.Repositories;

namespace TodoApp.Core.Validators;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    private readonly ITodoRepository _repository;

    public CreateTodoRequestValidator(ITodoRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Description)
            .NotEmpty()
            .DependentRules(() =>
            {
                RuleFor(x => x.Description)
                    .MustAsync(ValidateTodoAsync)
                    .OverridePropertyName("Todo")
                    .WithMessage("A 'Todo' with a duplicate 'Description' exists in the system");
            });
    }

    private async Task<bool> ValidateTodoAsync(string description, CancellationToken token)
    {
        var existing = await _repository.GetByDescriptionAsync(description, token);
        return existing is null;
    }
}