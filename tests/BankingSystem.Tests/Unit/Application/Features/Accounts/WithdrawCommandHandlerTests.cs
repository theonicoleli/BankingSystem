using BankingSystem.Application.Features.Accounts.Withdraw;
using BankingSystem.Domain;
using BankingSystem.Domain.Interfaces.Repository;
using BankingSystem.SharedKernel.Data;
using BankingSystem.SharedKernel.Domain.Exception;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingSystem.Tests.Unit.Application.Features.Accounts;

public class WithdrawCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly WithdrawCommandHandler _handler;

    public WithdrawCommandHandlerTests()
    {
        _repositoryMock = new Mock<IAccountRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new WithdrawCommandHandler(_repositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ExistingAccountWithBalance_ShouldDecreaseBalanceAndCommit()
    {
        var account = new Account("100", 50);
        var command = new WithdrawCommand("100", 20);
        _repositoryMock.Setup(r => r.GetById("100")).ReturnsAsync(account);

        var result = await _handler.Handle(command, CancellationToken.None);

        account.Balance.Should().Be(30);
        _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_NonExistingAccount_ShouldReturnFailure()
    {
        var command = new WithdrawCommand("999", 10);
        _repositoryMock.Setup(r => r.GetById("999")).ReturnsAsync((Account)null!);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        _uowMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_InsufficientFunds_ShouldThrowDomainException()
    {
        var account = new Account("100", 5);
        _repositoryMock.Setup(r => r.GetById("100")).ReturnsAsync(account);
        var command = new WithdrawCommand("100", 10);

        await FluentActions.Awaiting(() => _handler.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<DomainException>()
            .WithMessage("Insufficient funds.");

        _uowMock.Verify(u => u.CommitAsync(), Times.Never);
    }
}