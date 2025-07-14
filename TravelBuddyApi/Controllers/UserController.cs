namespace TravelBuddyApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using TravelBuddyApi.Models;
using TravelBuddyApi.Services.Interfaces;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService _userService) : ControllerBase
{
    //Get All action
    public async Task<IActionResult> GetAllUsers()
    {
        var getUsers = await _userService.GetAllUsersAsync();
        return Ok(getUsers);
    }

    //Get by Id action
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(long userId)
    {
        var getUser = await _userService.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            return NotFound();
        }
        return Ok(getUser);
    }

    // //Post: api/User
    // [HttpPost]
    // public async Task<IActionResult> AddUserAsync(long userId)
    // {
        
    // }
}