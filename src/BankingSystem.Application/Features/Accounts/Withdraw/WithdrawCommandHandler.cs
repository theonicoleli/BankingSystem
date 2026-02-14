using BankingSystem.Application.Dtos;
using BankingSystem.Application.Enums;
using BankingSystem.Application.Mappers;
using BankingSystem.Domain.Interfaces.Repository;
using BankingSystem.SharedKernel.Application;
using BankingSystem.SharedKernel.Application.Results;
using BankingSystem.SharedKernel.Data;
using MediatR;

namespace BankingSystem.Application.Features.Accounts.Withdraw;

public class WithdrawCommandHandler 
    : Handler<EventResponseDto>, IRequestHandler<WithdrawCommand, Result<EventResponseDto>>
{
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _uow;

    public WithdrawCommandHandler(IAccountRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Result<EventResponseDto>> Handle(WithdrawCommand request, CancellationToken ct)
    {
        var account = await _repository.GetById(request.OriginId);

        if (account is null)
            return Fail("Account not found");

        account.Withdraw(request.Amount);
        await _uow.CommitAsync();

        return Success(account.ToResponse(AccountDirection.Origin));
    }
}