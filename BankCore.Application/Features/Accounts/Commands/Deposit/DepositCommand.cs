using MediatR;

namespace BankCore.Application.Features.Accounts.Commands.Deposit;

public record DepositCommand(Guid AccountId, decimal Amount, string Currency) : IRequest<Unit>;