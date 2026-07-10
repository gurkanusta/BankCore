using BankCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankCore.Infrastructure.Persistence;

public class BankCoreDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public BankCoreDbContext(DbContextOptions<BankCoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankCoreDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}