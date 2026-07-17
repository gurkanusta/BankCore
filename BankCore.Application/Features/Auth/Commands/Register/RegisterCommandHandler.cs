using BankCore.Application.Abstractions;
using BankCore.Application.Identity;
using BankCore.Domain.Constants;
using BankCore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace BankCore.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            throw new InvalidOperationException(ErrorMessages.EmailAlreadyInUse);

        var customer = new Customer(request.FullName, request.NationalId, request.Email);
        await _customerRepository.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            CustomerId = customer.Id
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException(string.Format(ErrorMessages.UserCreationFailed, errors));
        }

        await _userManager.AddToRoleAsync(user, RoleNames.Customer);

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user, roles);

        return token;
    }
}