using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.Enums;
using BankCore.Domain.ValueObjects;
using BankCore.Domain.Exceptions;
using BankCore.Domain.Constants;



namespace BankCore.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string AccountNumber { get; private set; }
        public Money Balance { get; private set; }
        public bool IsActive { get; private set; }
        public AccountType Type { get; private set; }
        public byte[] RowVersion { get; private set; }= Array.Empty<byte>();
        private Account() { }

        public Account(Guid customerId, string accountNumber, Money initialBalance, AccountType type)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(ErrorMessages.AccountNumberCannotBeEmpty, nameof(accountNumber));
            }
            Id = Guid.NewGuid();
            CustomerId = customerId;
            AccountNumber = accountNumber;
            Balance = new Money(0, Constants.CurrencyCodes.TRY);
            IsActive = true;
            Type = type;
        }

        public void Deposit(Money amount)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException(ErrorMessages.InactiveAccount);
            }
            Balance = Balance.Add(amount);
        }

        public void Withdraw(Money amount)
        {
            EnsureActive();

            if (!Balance.IsGreaterOrEqualTo(amount))
            {
                throw new InvalidOperationException(ErrorMessages.InsufficientBalance);
            }
            Balance = Balance.Subtract(amount);
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        private void EnsureActive() { 
            if (!IsActive)
                throw new InvalidOperationException(ErrorMessages.InactiveAccountOperation);
        }
    }
}
