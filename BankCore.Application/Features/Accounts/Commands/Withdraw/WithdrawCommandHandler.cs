using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.ValueObjects;
using BankCore.Application.Abstractions;
using MediatR;
using BankCore.Domain.Entities;
using BankCore.Domain.Enums;
using BankCore.Domain.Constants;
using BankCore.Application.Exceptions;




namespace BankCore.Application.Features.Accounts.Commands.Withdraw;

public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;
    
    public WithdrawCommandHandler(IAccountRepository accountRepository,ICurrentUserService currentUserService ,ITransactionRepository transactionRepository,IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account == null)
        {
            throw new InvalidCastException(ErrorMessages.AccountNotFound);
        }

        if (!_currentUserService.IsAdmin && account.CustomerId != _currentUserService.CustomerId)
            throw new ForbiddenAccessException(ErrorMessages.AccessDenied);

        var amount = new Money(request.Amount, request.Currency);
        account.Withdraw(amount);

        var transaction= new Transaction(account.Id, TransactionType.Withdraw, amount);
        await _transactionRepository.AddAsync(transaction);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
// deposit ile aynı ama tek fark withdraw olmuş olması ileriye dönük bir şey eklemek istediğimde işimi koalylaştırır

