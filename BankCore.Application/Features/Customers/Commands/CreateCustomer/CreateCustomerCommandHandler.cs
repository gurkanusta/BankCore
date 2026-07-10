using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Application.Abstractions;
using BankCore.Domain.Entities;
using MediatR;

namespace BankCore.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand,Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.FullName, request.NationalId, request.Email);
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }
    }
}
