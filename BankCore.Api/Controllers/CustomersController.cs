using BankCore.Application.Features.Customers.Commands.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankCore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var customerId = await _mediator.Send(command);
        return Ok(customerId);
    }
}