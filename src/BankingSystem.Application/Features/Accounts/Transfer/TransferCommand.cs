using BankingSystem.Application.Abstraction.Command;
using BankingSystem.Application.Dtos;

namespace BankingSystem.Application.Features.Accounts.Transfer;

public class TransferCommand : Command<EventResponseDto>
{
    public TransferCommand(string originId, string destinationId, decimal amount)
    {
        OriginId = originId;
        DestinationId = destinationId;
        Amount = amount;
    }
    public string OriginId { get; }
    public string DestinationId { get; }
    public decimal Amount { get; }
}