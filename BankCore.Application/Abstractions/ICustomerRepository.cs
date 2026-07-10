using BankCore.Domain.Entities;

namespace BankCore.Application.Abstractions;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
}