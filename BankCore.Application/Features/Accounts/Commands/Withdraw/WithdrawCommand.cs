using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace BankCore.Application.Features.Accounts.Commands.Withdraw;

public record WithdrawCommand(Guid AccountId, decimal Amount, string Currency) : IRequest<Unit>; //ırequest işlem tamamlandı demesi için

    
