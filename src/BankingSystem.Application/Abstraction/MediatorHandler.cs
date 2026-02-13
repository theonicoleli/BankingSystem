using BankingSystem.SharedKernel.Application.Results;
using BankingSystem.Application.Abstraction.Command;
using MediatR;

namespace BankingSystem.Application.Abstraction;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<TResult>> Send<TCommand, TResult>(TCommand command)
        where TCommand : Command<TResult>
    {
        return await _mediator.Send(command);
    }

    public async Task<Result<TResponse>> Query<TQuery, TResponse>(TQuery query) 
        where TQuery : IRequest<Result<TResponse>>
    {
        return await _mediator.Send(query);
    }
}