using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.Entities;



namespace BankCore.Application.Abstractions;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
}