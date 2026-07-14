using System;
using System.Collections.Generic;
using BankCore.Domain.Entities;
using System.Text;

namespace BankCore.Application.Abstractions;

public interface IAuditLogRepository
{
    Task AddAsync(Domain.Entities.AuditLog auditLog);
}

