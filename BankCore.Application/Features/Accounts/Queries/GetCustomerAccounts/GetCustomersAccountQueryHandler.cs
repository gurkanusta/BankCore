using BankCore.Application.Abstractions;
using BankCore.Application.Features.Accounts.Queries.GetCustomerAccounts;
using BankCore.Application.Features.Customers.Queries.GetCustomerById;
using MediatR;
using BankCore.Application.Exceptions;
using MapsterMapper;
using BankCore.Domain.Constants;

public class GetCustomersAccountQueryHandler : IRequestHandler<GetCustomerAccountsQuery, List<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetCustomersAccountQueryHandler(IAccountRepository accountRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _currentUserService = _currentUserService;
        
    }

    public async Task<List<AccountDto>> Handle(GetCustomerAccountsQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAdmin && request.CustomerId != _currentUserService.CustomerId)
            throw new ForbiddenAccessException(ErrorMessages.AccessDenied);
        
        var accounts = await _accountRepository.GetByCustomerIdAsync(request.CustomerId);
        return _mapper.Map<List<AccountDto>>(accounts);
    }
}