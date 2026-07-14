using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Application.Abstractions;

namespace BankCore.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        void DiscardChanges();
    }
}
