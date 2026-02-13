using BankingSystem.SharedKernel.Application.Results;
using MediatR;

namespace BankingSystem.Application.Queries.Account.GetBalance;

public class GetBalanceQuery : IRequest<Result<decimal>>
{
    public GetBalanceQuery(string accountId)
    {
        AccountId = accountId;
    }
    public string AccountId { get; }
}