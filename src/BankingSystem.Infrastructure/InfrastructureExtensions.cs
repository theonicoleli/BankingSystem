using BankingSystem.Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BankingSystem.Infrastructure.Context;
using BankingSystem.Infrastructure.Repository;
using BankingSystem.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                }));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("BankingSystemDb"));
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}