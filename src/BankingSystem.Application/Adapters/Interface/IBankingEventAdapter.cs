using BankingSystem.Application.Dtos;
using BankingSystem.SharedKernel.Application.Results;

namespace BankingSystem.Application.Adapters.Interface;

public interface IBankingEventAdapter
{
    Task<Result<EventResponseDto>> ProcessEventAsync(EventRequestDto request);
}