using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace BankCore.Application.Features.Customers.Commands.CreateCustomer
{
    public record CreateCustomerCommand(string FullName, string NationalId, string Email) : IRequest<Guid>;
}
