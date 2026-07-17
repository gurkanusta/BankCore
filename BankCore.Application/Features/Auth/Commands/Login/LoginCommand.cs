using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace BankCore.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) : IRequest<string>;
