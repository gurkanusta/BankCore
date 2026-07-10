using BankCore.Application.Features.Accounts.Queries.GetCustomerAccounts;
using BankCore.Application.Features.Customers.Queries.GetCustomerById;
using BankCore.Domain.Entities;
using Mapster;

namespace BankCore.Application.Mappings;

public class MappingConfig : IRegister //ıregister implemente olan sınıfları otomatik buluyor
{
    public void Register(TypeAdapterConfig config) //config getiriyor içine kuralları yazıyorsun
    {
        config.NewConfig<Account, AccountDto>() //accountdan accountdtoya dönüşüm istendiğinde aşağıdaki şeyleri uygular
            .Map(dest => dest.Balance, src => src.Balance.Amount) 
            .Map(dest => dest.Currency, src => src.Balance.Currency)
            .Map(dest => dest.AccountType, src => src.Type.ToString());
    }
}