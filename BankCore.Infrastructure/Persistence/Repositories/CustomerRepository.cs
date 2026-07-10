using BankCore.Application.Abstractions;
using BankCore.Domain.Entities;
using BankCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankCore.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BankCoreDbContext _context;

    public CustomerRepository(BankCoreDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer); //nesneyi takibe al ama hemen yazma kaydetme işini application yapıo
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
    }
}