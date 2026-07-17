using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using BankCore.Domain.Constants;

namespace BankCore.Application.Features.Accounts.Commands.Withdraw;

public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
{
    public WithdrawCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty().WithMessage(ValidationMessages.AccountIdRequired);
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ValidationMessages.AmountMustBePositive);


        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage(ValidationMessages.CurrencyRequired)
            .Length(3).WithMessage(ValidationMessages.CurrencyLength);

    }
}
