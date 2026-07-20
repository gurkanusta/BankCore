using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Application.Abstractions;
using BankCore.Domain.Constants;
using BankCore.Domain.Entities;
using BankCore.Domain.Enums;
using BankCore.Domain.ValueObjects;
using MediatR;
using BankCore.Application.Exceptions;

namespace BankCore.Application.Features.Accounts.Commands.Transfer;

public class TransferCommandHandler : IRequestHandler<TransferCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public TransferCommandHandler(IAccountRepository accountRepository,ICurrentUserService currentUserService ,ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<Unit> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        var sourceAccount = await _accountRepository.GetByIdAsync(request.SourceAccountId);
        if (sourceAccount == null)
        {
            throw new InvalidCastException(ErrorMessages.SourceAccountNotFound);
        }

        if (!_currentUserService.IsAdmin && sourceAccount.CustomerId != _currentUserService.CustomerId)
            throw new ForbiddenAccessException(ErrorMessages.AccessDenied);

        var targetAccount = await _accountRepository.GetByIdAsync(request.TargetAccountId);
        if (targetAccount == null)
        {
            throw new InvalidCastException(ErrorMessages.TargetAccountNotFound);
        }
        var amount = new Money(request.Amount, request.Currency);
        sourceAccount.Withdraw(amount);
        targetAccount.Deposit(amount);
        var outgoingTransaction = new Transaction(sourceAccount.Id, TransactionType.TransferOut, new Money(request.Amount,request.Currency));
        var incomingTransaction = new Transaction(targetAccount.Id, TransactionType.TransferIn, new Money(request.Amount, request.Currency));
        await _transactionRepository.AddAsync(outgoingTransaction);
        await _transactionRepository.AddAsync(incomingTransaction);
        await _unitOfWork.SaveChangesAsync(cancellationToken); //dört değişikliği bir arada yapar ya 4 ün hepsi olucak ya da hiç olmayacak
        return Unit.Value;
    }
}

