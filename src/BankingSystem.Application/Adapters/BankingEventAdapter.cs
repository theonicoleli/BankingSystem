using BankingSystem.Application.Abstraction;
using BankingSystem.Application.Adapters.Interface;
using BankingSystem.Application.Dtos;
using BankingSystem.Application.Enums;
using BankingSystem.Application.Features.Accounts.Deposit;
using BankingSystem.Application.Features.Accounts.Transfer;
using BankingSystem.Application.Features.Accounts.Withdraw;
using BankingSystem.SharedKernel.Application.Results;

namespace BankingSystem.Application.Adapters;

public class BankingEventAdapter : IBankingEventAdapter
{
    private readonly IMediatorHandler _mediator;

    public BankingEventAdapter(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<EventResponseDto>> ProcessEventAsync(EventRequestDto request)
    {
        if (!Enum.TryParse<BankingEventType>(request.Type, true, out var eventType))
        {
            return Result<EventResponseDto>.Failure("Invalid event type");
        }
        
        return eventType switch
        {
            BankingEventType.Deposit => await _mediator.Send<DepositCommand, EventResponseDto>(
                new DepositCommand(request.Destination!, request.Amount)),

            BankingEventType.Withdraw => await _mediator.Send<WithdrawCommand, EventResponseDto>(
                new WithdrawCommand(request.Origin!, request.Amount)),

            BankingEventType.Transfer => await _mediator.Send<TransferCommand, EventResponseDto>(
                new TransferCommand(request.Origin!, request.Destination!, request.Amount)),

            _ => Result<EventResponseDto>.Failure("Invalid event type")
        };
    }
}