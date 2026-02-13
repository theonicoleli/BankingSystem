using BankingSystem.Application.Dtos;
using BankingSystem.Application.Enums;
using BankingSystem.Domain;

namespace BankingSystem.Application.Mappers;

public static class AccountMapper
{
    public static AccountDto ToDto(this Account account)
    {
        return new AccountDto 
        { 
            Id = account.Id, 
            Balance = account.Balance 
        };
    }

    public static EventResponseDto ToTransferResponse(this Account origin, Account destination)
    {
        return new EventResponseDto
        {
            Origin = origin.ToDto(),
            Destination = destination.ToDto()
        };
    }

    public static EventResponseDto ToResponse(this Account account, AccountDirection accountType)
    {
        return accountType switch
        {
            AccountDirection.Origin => new EventResponseDto { Origin = account.ToDto() },
            AccountDirection.Destination => new EventResponseDto { Destination = account.ToDto() },
            _ => throw new ArgumentOutOfRangeException(nameof(accountType), "Invalid account type for response")
        };
    }
}