using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.Constants;
using BankCore.Domain.Enums;
using BankCore.Domain.ValueObjects;

namespace BankCore.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }
        public TransactionType Type { get; private set; }
        public Money Amount { get; private set; }
        public DateTime CreatedDate { get; private set; }

        private Transaction() { }

        public Transaction(Guid accountId, TransactionType type, Money amount)
        {
            if (amount == null || amount.Amount <= 0)
            {
                throw new ArgumentException(ValidationMessages.InvalidTransactionAmount, nameof(amount));
            }
            Id = Guid.NewGuid();
            AccountId = accountId;
            Type = type;
            Amount = amount;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
