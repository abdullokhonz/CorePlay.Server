using CorePlay.implimintations.interfaces;
using Microsoft.AspNetCore.Mvc;
using CorePlay.Dtos;
namespace CorePlay.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUser _user;
    
    public UserController(IUser user) => _user = user;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] UserQuaryDtos user)
    {
        var res = await _user.GetAsync(user);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserCreateDro user)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var res = await _user.CreateAsync(user);
        
        if (res == null) return BadRequest("Error ro creating User");
        
        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _user.DeleteAsync(id);

        if (res == null) return null;
        
        return Ok(res);
    }
}