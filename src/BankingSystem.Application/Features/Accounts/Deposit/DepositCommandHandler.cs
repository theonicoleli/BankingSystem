using BankingSystem.Application.Dtos;
using BankingSystem.Application.Enums;
using BankingSystem.Application.Mappers;
using BankingSystem.Domain;
using BankingSystem.Domain.Interfaces.Repository;
using BankingSystem.SharedKernel.Application;
using BankingSystem.SharedKernel.Application.Results;
using BankingSystem.SharedKernel.Data;
using MediatR;

namespace BankingSystem.Application.Features.Accounts.Deposit;

public class DepositCommandHandler 
    : Handler<EventResponseDto>, IRequestHandler<DepositCommand, Result<EventResponseDto>>
{
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _uow;

    public DepositCommandHandler(IAccountRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Result<EventResponseDto>> Handle(DepositCommand request, CancellationToken ct)
    {
        var account = await _repository.GetById(request.DestinationId)
                      ?? Account.CreateNew(request.DestinationId);

        if (account.IsNew)
            await _repository.Add(account);

        account.Deposit(request.Amount);
        await _uow.CommitAsync();

        return Success(account.ToResponse(AccountDirection.Destination));
    }
}