using System;
using System.Collections.Generic;
using System.Text;
using BankCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankCore.Infrastructure.Persistence.Configurations;
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.CommandName).IsRequired().HasMaxLength(200);
        builder.Property(a => a.RequestData).IsRequired();
        builder.Property(a => a.IsSuccess).IsRequired();
        builder.Property(a => a.ErrorMessage).HasMaxLength(1000);
        builder.Property(a => a.ExecutedAt).IsRequired();
    }
}

