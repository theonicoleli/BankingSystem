using System.Text.Json.Serialization;
using BankingSystem.SharedKernel.Application.Results;
using MediatR;

namespace BankingSystem.Application.Abstraction.Command;

public abstract class Command<TResponse> : IRequest<Result<TResponse>>
{
    [JsonIgnore]
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    protected Command()
    {
        CommandId = Guid.NewGuid();
    }

    [JsonIgnore]
    public Guid CommandId { get; }
}