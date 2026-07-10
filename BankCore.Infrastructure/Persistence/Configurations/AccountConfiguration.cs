using BankCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankCore.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AccountNumber)
            .IsRequired()
            .HasMaxLength(26);

        builder.HasIndex(a => a.AccountNumber).IsUnique();

        builder.OwnsOne(a => a.Balance, money => //money tablosu açma onun yerine amount ve balance aç daha sade
        {
            money.Property(m => m.Amount).HasColumnName("Balance_Amount").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("Balance_Currency").HasMaxLength(3);
        });

        //builder.Property(a => a.RowVersion).IsRowVersion();
    }
}