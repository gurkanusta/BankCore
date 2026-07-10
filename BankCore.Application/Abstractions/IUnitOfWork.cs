using System;
using System.Collections.Generic;
using System.Text;

namespace BankCore.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
