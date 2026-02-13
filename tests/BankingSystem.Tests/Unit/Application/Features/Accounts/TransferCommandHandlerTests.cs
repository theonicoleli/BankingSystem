using BankingSystem.Application.Features.Accounts.Transfer;
using BankingSystem.Domain;
using BankingSystem.Domain.Interfaces.Repository;
using BankingSystem.SharedKernel.Data;
using BankingSystem.SharedKernel.Domain.Exception;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingSystem.Tests.Unit.Application.Features.Accounts;

public class TransferCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly TransferCommandHandler _handler;

    public TransferCommandHandlerTests()
    {
        _repositoryMock = new Mock<IAccountRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _handler = new TransferCommandHandler(_repositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ValidTransfer_ShouldUpdateBothAccounts()
    {
        var origin = new Account("100", 100);
        var destination = new Account("200");
        
        _repositoryMock.Setup(r => r.GetById("100")).ReturnsAsync(origin);
        _repositoryMock.Setup(r => r.GetById("200")).ReturnsAsync(destination);

        var command = new TransferCommand("100", "200", 30);

        var result = await _handler.Handle(command, CancellationToken.None);

        origin.Balance.Should().Be(70);
        destination.Balance.Should().Be(30);
        _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task Handle_InsufficientFunds_ShouldThrowDomainException()
    {
        var origin = new Account("100", 10);
        var destination = new Account("200", 0);
        var command = new TransferCommand("100", "200", 50);

        _repositoryMock.Setup(r => r.GetById("100")).ReturnsAsync(origin);
        _repositoryMock.Setup(r => r.GetById("200")).ReturnsAsync(destination);

        await FluentActions.Awaiting(() => _handler.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<DomainException>()
            .WithMessage("Insufficient funds.");

        _uowMock.Verify(u => u.CommitAsync(), Times.Never);
    }
}