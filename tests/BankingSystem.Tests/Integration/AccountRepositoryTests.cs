using BankingSystem.Infrastructure.Context;
using BankingSystem.Infrastructure.Repository;
using BankingSystem.Domain;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Testcontainers.PostgreSql;
using Xunit;

namespace BankingSystem.Tests.Integration;

public class AccountRepositoryTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:15")
        .Build();

    public async Task InitializeAsync() => await _container.StartAsync();
    public async Task DisposeAsync() => await _container.DisposeAsync();

    [Fact]
    public async Task Integration_Deposit_ShouldPersistInPostgres()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        using var context = new AppDbContext(options);
        await context.Database.EnsureCreatedAsync();
        
        var repo = new AccountRepository(context);
        var account = new Account("456", 0);

        await repo.Add(account);
        await context.SaveChangesAsync();

        var result = await repo.GetById("456");
        result?.Balance.Should().Be(0);
    }
}