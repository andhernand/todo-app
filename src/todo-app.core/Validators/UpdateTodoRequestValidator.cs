using FluentValidation;

using TodoApp.Contracts.Requests;

namespace TodoApp.Core.Validators;

public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
{
    public UpdateTodoRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}