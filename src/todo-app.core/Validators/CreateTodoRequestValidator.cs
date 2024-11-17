using FluentValidation;

using TodoApp.Contracts.Requests;

namespace TodoApp.Core.Validators;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty();
    }
}