using BankingSystem.Application.Adapters.Interface;
using BankingSystem.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.WebApi.Controllers;

[ApiController]
[Route("event")]
public class EventController : ControllerBase
{
    private readonly IBankingEventAdapter _eventAdapter;

    public EventController(IBankingEventAdapter eventAdapter)
    {
        _eventAdapter = eventAdapter;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EventRequestDto request)
    {
        var result = await _eventAdapter.ProcessEventAsync(request);

        if (!result.IsSuccess)
            return NotFound(0);

        return Created(string.Empty, result.Data);
    }
}