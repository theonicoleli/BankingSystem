using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BankingSystem.Domain;
using BankingSystem.SharedKernel.Domain;

namespace BankingSystem.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(Entity.UpdatedAt))
                    .ValueGeneratedNever();
                
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(Entity.IsRemoved));
                var filter = Expression.Lambda(
                    Expression.Equal(property, Expression.Constant(false)), parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}