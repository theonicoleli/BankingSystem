using BankingSystem.Domain.Interfaces.Repository;
using BankingSystem.SharedKernel.Application.Results;
using MediatR;

namespace BankingSystem.Application.Queries.Account.GetBalance;

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, Result<decimal>>
{
    private readonly IAccountRepository _repository;

    public GetBalanceQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<decimal>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetById(request.AccountId);

        if (account is null)
            return Result<decimal>.Failure("Account not found");

        return Result<decimal>.Success(account.Balance);
    }
}