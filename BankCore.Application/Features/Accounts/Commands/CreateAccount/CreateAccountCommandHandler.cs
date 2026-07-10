using BankCore.Application.Abstractions;
using BankCore.Domain.Entities;
using BankCore.Domain.Enums;
using BankCore.Domain.ValueObjects;
using MediatR;

namespace BankCore.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer is null)
            throw new InvalidOperationException("Belirtilen müşteri bulunamadı.");

        var accountNumber = GenerateAccountNumber();

        var initialBalance = new Money(0, "TRY");
        var account = new Account(customer.Id, accountNumber, initialBalance, (AccountType)request.AccountType);
        await _accountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Id;
    }

    private static string GenerateAccountNumber()
    {
        return "TR" + Random.Shared.NextInt64(100000000000, 999999999999);
    }
}   