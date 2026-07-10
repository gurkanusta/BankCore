using BankCore.Application.Abstractions;
using BankCore.Domain.ValueObjects;
using MediatR;

namespace BankCore.Application.Features.Accounts.Commands.Deposit;

public class DepositCommandHandler : IRequestHandler<DepositCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DepositCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account is null)
            throw new InvalidOperationException("Hesap bulunamadı.");

        var amount = new Money(request.Amount, request.Currency);
        account.Deposit(amount);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}