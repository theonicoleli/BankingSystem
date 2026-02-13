using BankingSystem.Application.Abstraction.Command;
using BankingSystem.Application.Dtos;

namespace BankingSystem.Application.Features.Accounts.Withdraw;

public class WithdrawCommand : Command<EventResponseDto>
{
    public WithdrawCommand(string originId, decimal amount)
    {
        OriginId = originId;
        Amount = amount;
    }
    public string OriginId { get; }
    public decimal Amount { get; }
}