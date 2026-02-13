using BankingSystem.Application.Abstraction;
using BankingSystem.Application.Queries.Account.GetBalance;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.WebApi.Controllers;

[ApiController]
[Route("balance")]
public class BalanceController : ControllerBase
{
    private readonly IMediatorHandler _mediator;

    public BalanceController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string account_id)
    {
        var query = new GetBalanceQuery(account_id);
        var result = await _mediator.Query<GetBalanceQuery, decimal>(query);

        if (!result.IsSuccess)
            return NotFound(0);

        return Ok(result.Data);
    }
}