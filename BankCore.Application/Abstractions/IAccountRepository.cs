using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.Entities;


namespace BankCore.Application.Abstractions
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task<Account?> GetByIdAsync(Guid id);

        Task<List<Account>> GetByCustomerIdAsync(Guid customerId);
    }
}
