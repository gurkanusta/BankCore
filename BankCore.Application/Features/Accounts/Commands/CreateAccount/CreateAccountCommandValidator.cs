using FluentValidation;
using BankCore.Domain.Constants;
namespace BankCore.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage(ValidationMessages.AccountIdRequired);

        RuleFor(x => x.AccountType)
            .InclusiveBetween(1, 2).WithMessage(ValidationMessages.AccountTypeInvalid);
    }
}