using BankCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankCore.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.OwnsOne(t => t.Amount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("Amount_Value").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("Amount_Currency").HasMaxLength(3);
        });
    }
}