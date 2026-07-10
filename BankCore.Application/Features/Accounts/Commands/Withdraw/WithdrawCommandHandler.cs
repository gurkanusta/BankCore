using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.ValueObjects;
using BankCore.Application.Abstractions;
using MediatR;



namespace BankCore.Application.Features.Accounts.Commands.Withdraw;

public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    public WithdrawCommandHandler(IAccountRepository accountRepository,IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account == null)
        {
            throw new InvalidCastException("Hesap bulunamadı.");
        }
        var amount = new Money(request.Amount, request.Currency);
        account.Withdraw(amount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
// deposit ile aynı ama tek fark withdraw olmuş olması ileriye dönük bir şey eklemek istediğimde işimi koalylaştırır

