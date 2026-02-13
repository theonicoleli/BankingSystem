using BankingSystem.Application.Abstraction;
using BankingSystem.Application.Adapters;
using BankingSystem.Application.Dtos;
using BankingSystem.Application.Features.Accounts.Deposit;
using BankingSystem.SharedKernel.Application.Results;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingSystem.Tests.Unit.Application;

public class BankingEventAdapterTests
{
    private readonly Mock<IMediatorHandler> _mediatorMock;
    private readonly BankingEventAdapter _adapter;

    public BankingEventAdapterTests()
    {
        _mediatorMock = new Mock<IMediatorHandler>();
        _adapter = new BankingEventAdapter(_mediatorMock.Object);
    }

    [Fact]
    public async Task ProcessEventAsync_WhenTypeIsDeposit_ShouldSendDepositCommand()
    {
        var request = new EventRequestDto { Type = "deposit", Destination = "100", Amount = 10 };
        var expectedResponse = new EventResponseDto {};
    
        _mediatorMock
            .Setup(m => m.Send<DepositCommand, EventResponseDto>(It.IsAny<DepositCommand>()))
            .ReturnsAsync(Result<EventResponseDto>.Success(expectedResponse));

        var result = await _adapter.ProcessEventAsync(request);

        _mediatorMock.Verify(m => m.Send<DepositCommand, EventResponseDto>(
            It.Is<DepositCommand>(c => c.DestinationId == "100" && c.Amount == 10)), Times.Once);
    
        result.IsSuccess.Should().BeTrue();
    }
}