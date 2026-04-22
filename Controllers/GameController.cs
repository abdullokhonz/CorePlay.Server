using CorePlay.Dtos;
using CorePlay.implimintations.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CorePlay.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGame _gameService;

    public GameController(IGame gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GameQuaryDto dto)
    {
        var result = await _gameService.GetAsync(dto);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GameCreateDto dto)
    {
        var result = await _gameService.CreateAsync(dto);

        if (result == null)
            return BadRequest("Invalid data or game already exists.");

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GameUpdateDto dto)
    {
        var result = await _gameService.UpdateAsync(id, dto);

        if (result == null)
            return NotFound("Game not found.");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _gameService.DeleteAsync(id);

        if (result == null)
            return NotFound("Game not found.");

        return Ok(result);
    }
}