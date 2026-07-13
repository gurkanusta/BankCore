using System;
using System.Collections.Generic;
using System.Text;
using MediatR;


namespace BankCore.Application.Features.Accounts.Commands.Transfer;

public record TransferCommand(Guid SourceAccountId, Guid TargetAccountId, decimal Amount, string Currency) : IRequest<Unit>;