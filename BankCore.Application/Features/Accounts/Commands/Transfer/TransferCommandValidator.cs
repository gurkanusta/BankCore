using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using BankCore.Domain.Constants;

namespace BankCore.Application.Features.Accounts.Commands.Transfer;

public class TransferCommandValidator : AbstractValidator<TransferCommand>
{
    public TransferCommandValidator()
    {
        RuleFor(x => x.SourceAccountId).NotEmpty().WithMessage(ValidationMessages.SourceAccountRequired);
        RuleFor(x => x.TargetAccountId).NotEmpty().WithMessage(ValidationMessages.TargetAccountRequired);
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ValidationMessages.AmountMustBePositive);
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage(ValidationMessages.CurrencyRequired)
            .Length(3).WithMessage(ValidationMessages.CurrencyLength);

        RuleFor(x => x)
            .Must(x => x.SourceAccountId != x.TargetAccountId)
            .WithMessage(ValidationMessages.SourceTargetSameAccount);


    }
}