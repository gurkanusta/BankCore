using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankCore.Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    string FullName,
    string NationalId,
    string Email,
    string Password) : IRequest<string>;
