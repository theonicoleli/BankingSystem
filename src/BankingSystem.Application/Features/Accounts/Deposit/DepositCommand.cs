using BankingSystem.Application.Abstraction.Command;
using BankingSystem.Application.Dtos;

namespace BankingSystem.Application.Features.Accounts.Deposit;

public class DepositCommand : Command<EventResponseDto>
{
    public DepositCommand(string destinationId, decimal amount)
    {
        DestinationId = destinationId;
        Amount = amount;
    }
    public string DestinationId { get; }
    public decimal Amount { get; }
}