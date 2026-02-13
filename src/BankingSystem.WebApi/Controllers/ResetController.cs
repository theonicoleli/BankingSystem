using BankingSystem.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.WebApi.Controllers;

[ApiController]
[Route("reset")]
public class ResetController : ControllerBase
{
    private readonly IAccountRepository _repository;

    public ResetController(IAccountRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Reset()
    {
        await _repository.Reset();
        return Content("OK");
    }
}