using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // POST api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    // GET api/users/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserAsync(id);

        if (user == null) return NotFound();

        return Ok(user);
    }

    // POST api/users/{id}/roles
    [HttpPost("{id:int}/roles")]
    public async Task<IActionResult> AssignRole(int id, [FromBody] int roleId)
    {
        await _userService.AssignRoleAsync(id, roleId);
        return NoContent();
    }
}