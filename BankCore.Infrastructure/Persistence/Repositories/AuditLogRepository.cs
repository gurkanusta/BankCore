using System;
using System.Collections.Generic;
using System.Text;

namespace BankCore.Infrastructure.Persistence.Repositories;

public class AuditLogRepository : BankCore.Application.Abstractions.IAuditLogRepository
{
    private readonly BankCoreDbContext _context;
    public AuditLogRepository(BankCoreDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Domain.Entities.AuditLog auditLog)
    {
        await _context.AuditLogs.AddAsync(auditLog);
        await _context.SaveChangesAsync();
    }
}

