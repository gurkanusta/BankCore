using BankCore.Domain.Constants;
using FluentValidation;

namespace BankCore.Application.Features.Accounts.Commands.Deposit;

public class DepositCommandValidator : AbstractValidator<DepositCommand>
{
    public DepositCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage(ValidationMessages.AccountIdRequired);

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage(ValidationMessages.AmountMustBePositive);

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage(ValidationMessages.CurrencyRequired)
            .Length(3).WithMessage(ValidationMessages.CurrencyLength);
    }
}