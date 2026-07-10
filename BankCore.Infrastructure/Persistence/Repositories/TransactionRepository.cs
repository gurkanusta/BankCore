using System;
using System.Collections.Generic;
using BankCore.Domain.Entities;
using BankCore.Application.Abstractions;
using BankCore.Infrastructure.Persistence;
using System.Text;

namespace BankCore.Infrastructure.Persistence.Repositories;


public class TransactionRepository : ITransactionRepository
{
    private readonly BankCoreDbContext _context;
    public TransactionRepository(BankCoreDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }
}
