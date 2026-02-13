using BankingSystem.Application.Features.Accounts.Deposit;
using BankingSystem.Domain;
using BankingSystem.Domain.Interfaces.Repository;
using BankingSystem.SharedKernel.Data;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingSystem.Tests.Unit.Application.Features.Accounts;

public class DepositCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly DepositCommandHandler _handler;

    public DepositCommandHandlerTests()
    {
        _repositoryMock = new Mock<IAccountRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new DepositCommandHandler(_repositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ExistingAccount_ShouldUpdateBalanceAndCommit()
    {
        var account = new Account("100", 20);
        var command = new DepositCommand("100", 10);
        _repositoryMock.Setup(r => r.GetById("100")).ReturnsAsync(account);

        var result = await _handler.Handle(command, CancellationToken.None);

        account.Balance.Should().Be(30);
        _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_NewAccount_ShouldCreateAndCommit()
    {
        var command = new DepositCommand("200", 50);
        _repositoryMock.Setup(r => r.GetById("200")).ReturnsAsync((Account)null!);

        await _handler.Handle(command, CancellationToken.None);

        _repositoryMock.Verify(r => r.Add(It.Is<Account>(a => a.Id == "200" && a.Balance == 50)), Times.Once);
        _uowMock.Verify(u => u.CommitAsync(), Times.Once);
    }
}