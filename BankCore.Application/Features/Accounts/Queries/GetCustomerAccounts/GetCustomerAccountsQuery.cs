using MediatR;
using System;
using BankCore.Application.Features.Customers.Queries.GetCustomerById;


namespace BankCore.Application.Features.Accounts.Queries.GetCustomerAccounts;
public record GetCustomerAccountsQuery(Guid CustomerId) : IRequest<List<AccountDto>>;

//veriyi döndür
