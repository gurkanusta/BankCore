using FluentValidation;
using BankCore.Domain.Constants;
namespace BankCore.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.FullNameTooLong);

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage(ValidationMessages.NationalIdLength)
            .Length(11).WithMessage(ValidationMessages.NationalIdLength)
            .Matches("^[0-9]+$").WithMessage(ValidationMessages.NationalIdDigitsOnly);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
            .EmailAddress().WithMessage(ValidationMessages.EmailInvalid);
    }
}