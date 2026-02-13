using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankingSystem.Domain;

namespace BankingSystem.Infrastructure.Context.Mappings;

public class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .IsRequired()
            .HasMaxLength(50)
            .ValueGeneratedNever();

        builder.Property(a => a.Balance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsConcurrencyToken()
            .IsRequired(false);
        
        builder.Property(a => a.RemovedAt)
            .IsRequired(false);

        builder.Property(a => a.IsRemoved)
            .IsRequired();
            
        builder.Ignore("BalanceLock"); 
        builder.Ignore(a => a.IsNew);
    }
}