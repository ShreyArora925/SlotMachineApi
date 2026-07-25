using Microsoft.AspNetCore.Mvc;
using SlotMachineApi.Application.Interfaces;
using SlotMachineApi.Application.Models;

namespace SlotMachineApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("spin")]
    public async Task<IActionResult> Spin([FromBody] SpinRequest request)
    {
        try
        {
            var result = await _gameService.SpinAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("balance")]
    public async Task<IActionResult> UpdateBalance([FromBody] UpdateBalanceRequest request)
    {
        try
        {
            var result = await _gameService.UpdateBalanceAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        try
        {
            var result = await _gameService.SeedAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}