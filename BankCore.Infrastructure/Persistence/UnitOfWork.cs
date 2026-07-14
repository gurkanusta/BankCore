using BankCore.Application.Abstractions;

namespace BankCore.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly BankCoreDbContext _context;

    public UnitOfWork(BankCoreDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void DiscardChanges()
    {
        _context.ChangeTracker.Clear();
    }
}