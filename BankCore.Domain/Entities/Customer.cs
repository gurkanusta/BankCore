using BankCore.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankCore.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string NationalId { get; private set; }
        public string Email { get; private set; }

        public Customer() { }

        public Customer(string fullName, string nationalId, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException(ValidationMessages.FullNameRequired, nameof(fullName));
            }

            if (string.IsNullOrWhiteSpace(nationalId) || nationalId.Length != 11)
                throw new ArgumentException(ValidationMessages.NationalIdLength, nameof(nationalId));


            Id = Guid.NewGuid();
            FullName = fullName;
            NationalId = nationalId;
            Email = email;
        }
    }
}
