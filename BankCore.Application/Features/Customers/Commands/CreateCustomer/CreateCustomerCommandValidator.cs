using FluentValidation;

namespace BankCore.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad soyad boş bırakılamaz.")
            .MaximumLength(100).WithMessage("Ad soyad 100 karakterden uzun olamaz.");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("TC Kimlik No boş bırakılamaz.")
            .Length(11).WithMessage("TC Kimlik No 11 haneli olmalıdır.")
            .Matches("^[0-9]+$").WithMessage("TC Kimlik No yalnızca rakamlardan oluşmalıdır.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
    }
}