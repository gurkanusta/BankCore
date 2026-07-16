using BankCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BankCore.Application.Identity;
namespace BankCore.Infrastructure.Persistence;

public class BankCoreDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public BankCoreDbContext(DbContextOptions<BankCoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankCoreDbContext).Assembly);
        
    }
}