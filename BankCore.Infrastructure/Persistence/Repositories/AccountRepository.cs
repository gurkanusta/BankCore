using BankCore.Application.Abstractions;
using BankCore.Domain.Entities;
using BankCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankCore.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BankCoreDbContext _context;

    public AccountRepository(BankCoreDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Account>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _context.Accounts
            .Where(a => a.CustomerId == customerId) //sql de karşılığı select * from Accounts where CustomerId = @customerId
            .ToListAsync();
    }
}