using BankCore.Application.Features.Accounts.Commands.CreateAccount;
using BankCore.Application.Features.Accounts.Queries.GetCustomerAccounts;
using BankCore.Application.Features.Accounts.Commands.Deposit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankCore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        var accountId = await _mediator.Send(command);
        return Ok(accountId);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetCustomer(Guid customerId)
    {
        
        var accounts = await _mediator.Send(new GetCustomerAccountsQuery(customerId));
        return Ok(accounts);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}