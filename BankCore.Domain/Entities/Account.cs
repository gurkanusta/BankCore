using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.Enums;
using BankCore.Domain.ValueObjects;
using BankCore.Domain.Exceptions;

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

        private Account() { }

        public Account(Guid customerId, string accountNumber, Money initialBalance, AccountType type)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Hesap numarası boş olamaz.", nameof(accountNumber));
            }
            Id = Guid.NewGuid();
            CustomerId = customerId;
            AccountNumber = accountNumber;
            Balance = new Money(0,"TRY");
            IsActive = true;
            Type = type;
        }

        public void Deposit(Money amount)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Hesap aktif değil.");
            }
            Balance = Balance.Add(amount);
        }

        public void Withdraw(Money amount)
        {
            EnsureActive();

            if (!Balance.IsGreaterOrEqualTo(amount))
            {
                throw new InvalidOperationException("Yetersiz bakiye.");
            }
            Balance = Balance.Subtract(amount);
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        private void EnsureActive() { 
            if (!IsActive)
                throw new InvalidOperationException("Hesap aktif değil.");
        }
    }
}
