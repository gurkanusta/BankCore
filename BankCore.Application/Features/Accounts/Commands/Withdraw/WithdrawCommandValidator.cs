using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace BankCore.Application.Features.Accounts.Commands.Withdraw;

public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
{
    public WithdrawCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("Hesap seçilmelidir.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Çekilecek miktar sıfırdan büyük olmalıdır.");


        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Para birimi belirtilmelidir.")
            .Length(3).WithMessage("Para birimi 3 karakter olmalıdır.");

    }
}
