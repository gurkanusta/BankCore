using FluentValidation;

namespace BankCore.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Müşteri seçilmelidir.");

        RuleFor(x => x.AccountType)
            .InclusiveBetween(1, 2).WithMessage("Geçersiz hesap türü.");
    }
}