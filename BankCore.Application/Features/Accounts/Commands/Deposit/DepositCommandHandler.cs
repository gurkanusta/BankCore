using BankCore.Application.Abstractions;
using BankCore.Domain.Enums;
using BankCore.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using BankCore.Domain.Entities;
using BankCore.Domain.Constants;



namespace BankCore.Application.Features.Accounts.Commands.Deposit;

public class DepositCommandHandler : IRequestHandler<DepositCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ILogger<DepositCommandHandler> _logger;


    public DepositCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, ITransactionRepository transactionRepository, ILogger<DepositCommandHandler> logger)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account is null)
            throw new InvalidOperationException(ErrorMessages.AccountNotFound);

        var amount = new Money(request.Amount, request.Currency);
        account.Deposit(amount);
        var transaction = new Domain.Entities.Transaction(account.Id, TransactionType.Deposit, amount);
        await _transactionRepository.AddAsync(transaction); // izlemeye al

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}