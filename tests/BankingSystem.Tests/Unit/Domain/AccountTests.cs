using BankingSystem.Domain;
using FluentAssertions;
using Xunit;

namespace BankingSystem.Tests.Unit.Domain;

public class AccountTests
{
    [Fact]
    public void CreateNew_ShouldInitializeWithCorrectValues()
    {
        var account = Account.CreateNew("100");

        account.Id.Should().Be("100");
        account.Balance.Should().Be(0);
        account.IsNew.Should().BeTrue();
    }

    [Fact]
    public void Deposit_ShouldIncreaseBalance()
    {
        var account = new Account("100", 10);
        
        account.Deposit(20);

        account.Balance.Should().Be(30);
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalance_WhenFundsAreAvailable()
    {
        var account = new Account("100", 50);
        
        account.Withdraw(20);

        account.Balance.Should().Be(30);
    }

    [Fact]
    public void TransferTo_ShouldUpdateBothBalances()
    {
        var origin = new Account("100", 100);
        var destination = new Account("200", 0);

        origin.TransferTo(destination, 30);

        origin.Balance.Should().Be(70);
        destination.Balance.Should().Be(30);
    }
}