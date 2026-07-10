using BankCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankCore.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // primary keydir.
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FullName)
            .IsRequired() //null olmaz
            .HasMaxLength(100);

        builder.Property(c => c.NationalId)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(c => c.NationalId).IsUnique(); //tc nin benzersiz sağlar
    }
}