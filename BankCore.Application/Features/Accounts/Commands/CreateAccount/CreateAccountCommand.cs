using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace BankCore.Application.Features.Accounts.Commands.CreateAccount;


public record CreateAccountCommand(Guid CustomerId, int AccountType) : IRequest<Guid>;



