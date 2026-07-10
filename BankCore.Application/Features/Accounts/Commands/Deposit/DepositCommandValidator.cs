using FluentValidation;

namespace BankCore.Application.Features.Accounts.Commands.Deposit;

public class DepositCommandValidator : AbstractValidator<DepositCommand>
{
    public DepositCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Hesap seçilmelidir.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Tutar sıfırdan büyük olmalıdır.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Para birimi belirtilmelidir.")
            .Length(3).WithMessage("Para birimi 3 karakter olmalıdır (örn. TRY).");
    }
}