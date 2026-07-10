using BankCore.Application.Abstractions;
using BankCore.Application.Features.Accounts.Queries.GetCustomerAccounts;
using BankCore.Application.Features.Customers.Queries.GetCustomerById;
using MediatR;

using MapsterMapper;

public class GetCustomerAccountQueryHandler : IRequestHandler<GetCustomerAccountsQuery, List<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetCustomerAccountQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<List<AccountDto>> Handle(GetCustomerAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetByCustomerIdAsync(request.CustomerId);
        return _mapper.Map<List<AccountDto>>(accounts);
    }
}