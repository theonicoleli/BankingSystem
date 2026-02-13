using BankingSystem.SharedKernel.Application.Results;
using BankingSystem.Application.Abstraction.Command;
using MediatR;

namespace BankingSystem.Application.Abstraction;

public interface IMediatorHandler
{
    Task<Result<TResult>> Send<TCommand, TResult>(TCommand command)
        where TCommand : Command<TResult>;

    Task<Result<TResponse>> Query<TQuery, TResponse>(TQuery query)
        where TQuery : IRequest<Result<TResponse>>;
}