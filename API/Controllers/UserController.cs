using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserIRepository _userRepository;

    public UserController(UserIRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        await _userRepository.AddUser(user);
        return Ok();    
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.UserId)
        {
            return BadRequest();
        }

        try
        {
            await _userRepository.UpdateUser(user);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userRepository.DeleteUser(id);

        return NoContent();
    }
}