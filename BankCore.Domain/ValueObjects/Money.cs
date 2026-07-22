using BankCore.Domain.Constants;

namespace BankCore.Domain.ValueObjects;


public class Money {
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency) {
        if (amount < 0)
        {
            throw new ArgumentException(ValidationMessages.AmountMustBePositive, nameof(amount));
        }
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException(ValidationMessages.CurrencyRequired, nameof(currency));
        }
        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }
     public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        if (Amount < other.Amount)
        {
            throw new InvalidOperationException(ValidationMessages.SubtractionOperationCannotBeNegative);
        }
        return new Money(Amount - other.Amount, Currency);
    }

    public bool IsGreaterOrEqualTo(Money other) {
        EnsureSameCurrency(other);
        return Amount >= other.Amount;
    }
    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException(ValidationMessages.DontUseDifferentCurrency);
    }

    public override bool Equals(object? obj)
    {
        return obj is Money other && Amount == other.Amount && Currency == other.Currency;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }


}
