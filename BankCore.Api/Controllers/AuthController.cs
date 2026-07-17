using Microsoft.AspNetCore.Mvc;
using BankCore.Application.Features.Auth.Commands.Login;
using BankCore.Application.Features.Auth.Commands.Register;
using MediatR;


namespace BankCore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { Token = token });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { Token = token });
    }
}



