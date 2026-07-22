using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.Constants;
using FluentValidation;

namespace BankCore.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator: AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
            .NotEmpty().WithMessage(ValidationMessages.EmailInvalid);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.PasswordRequired);
    }
}
