using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace BankCore.Application.Features.Accounts.Commands.Transfer;

public class TransferCommandValidator : AbstractValidator<TransferCommand>
{
    public TransferCommandValidator()
    {
        RuleFor(x => x.SourceAccountId).NotEmpty().WithMessage("Kaynak hesap seçilmelidir.");
        RuleFor(x => x.TargetAccountId).NotEmpty().WithMessage("Hedef hesap seçilmelidir.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Transfer miktarı sıfırdan büyük olmalıdır.");
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Para birimi boş olamaz.")
            .Length(3).WithMessage("Para birimi 3 karakter olmalıdır.");

        RuleFor(x => x)
            .Must(x => x.SourceAccountId != x.TargetAccountId)
            .WithMessage("Kaynak ve hedef hesap aynı olamaz.");


    }
}