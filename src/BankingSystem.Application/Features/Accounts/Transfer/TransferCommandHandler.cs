using BankingSystem.Application.Dtos;
using BankingSystem.Application.Mappers;
using BankingSystem.Domain;
using BankingSystem.Domain.Interfaces.Repository;
using BankingSystem.SharedKernel.Application;
using BankingSystem.SharedKernel.Application.Results;
using BankingSystem.SharedKernel.Data;
using MediatR;

namespace BankingSystem.Application.Features.Accounts.Transfer;

public class TransferCommandHandler 
    : Handler<EventResponseDto>, IRequestHandler<TransferCommand, Result<EventResponseDto>>
{
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _uow;

    public TransferCommandHandler(IAccountRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Result<EventResponseDto>> Handle(TransferCommand request, CancellationToken ct)
    {
        var origin = await _repository.GetById(request.OriginId);
        
        if (origin is null) 
            return Fail("Origin account not found.");

        var destination = await _repository.GetById(request.DestinationId) 
                          ?? Account.CreateNew(request.DestinationId);
    
        if (destination.IsNew)
            await _repository.Add(destination);

        origin.TransferTo(destination, request.Amount);
        await _uow.CommitAsync();

        return Success(origin.ToTransferResponse(destination));
    }
}