namespace TravelBuddyApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService _userService) : ControllerBase
{
    //Get All action
    [HttpGet("getusers")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var getUsers = await _userService.GetAllUsersAsync();
            return Ok(getUsers);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get by Id action
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByIdAsync(long userId)
    {
        try
        {
            var getUser = await _userService.GetUserByIdAsync(userId);
            if (getUser == null)
            {
                return NotFound();
            }
            return Ok(getUser);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get by hobbies
    [HttpGet("filter-by-hobbies")]
    public async Task<IActionResult> GetUsersWithSpecificHobbies([FromBody] IEnumerable<long> hobbyIds)
    {
        //Check if hobbyIds is empty
        if (!hobbyIds.Any())
        {
            return BadRequest();
        }

        try
        {
            var users = await _userService.GetUsersWithSpecificHobbies(hobbyIds);
            return Ok(users);
        }
        catch (Exception)
        {
            return StatusCode(500, "Error filtering users.");
        }
    }

    //Post: api/User
    [HttpPost("adduser")]
    public async Task<IActionResult> AddUserAsync([FromBody] UserCreateDTO userCreateDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var createdUser = await _userService.AddUserAsync(userCreateDTO);
            if (createdUser == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetUserByIdAsync), new { userId = createdUser.UserId }, createdUser);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Put: api/User/{id}
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUserAsync(long userId, [FromBody] UserUpdateDTO userUpdateDTO)
    {
        if (!ModelState.IsValid)
        {
            //This will trigger if the data dto sent from the frontend is not in the format required. 
            //e.g. some fields are not filled, and a field that requires text, but got numbers instead.
            return BadRequest(ModelState);
        }
        try
        {
            var updatedResponse = await _userService.UpdateUserAsync(userId, userUpdateDTO);
            if (updatedResponse == null)
            {
                //This will trigger if UserService UpdateUserAsync() could not find the user, which means that the user does not exist.
                return NotFound();
            }

            //This will trigger if everything works as expected.
            return Ok(updatedResponse);
        }
        catch (Exception)
        {
            //This will trigger if anything else happens "StatusCode(500), e.g. the server times out, the write to the db has failed etc.
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user."
        );
        }
    }

    [HttpPut("{userId}/change-password")]
    public async Task<IActionResult> ChangePasswordAsync(long userId, [FromBody] PasswordUpdateDTO passwordUpdateDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            await _userService.ChangePasswordAsync(userId, passwordUpdateDTO);
            return Ok("Password has been updated successfully");
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user."
        );
        }

    }

    [HttpGet("{userId}/{hobbyId}")]
    public async Task<IActionResult> GetUserHobbyAsync(long userId, long hobbyId)
    {
        try
        {
            var getHobby = await _userService.GetUserHobbyAsync(userId, hobbyId);
            if (getHobby == null)
            {
                return NotFound();
            }
            return Ok(getHobby);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveUserAsync(long userId)
    {
        try
        {
            var didRemove = await _userService.RemoveUserAsync(userId);
            if (!didRemove)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user."
        );
        }
    }

    [HttpPost("{userId}/add/{hobbyId}")]
    public async Task<IActionResult> AddHobbyToUserAsync(long userId, long hobbyId)
    {
        try
        {
            var addHobby = await _userService.AddHobbyToUserAsync(userId, hobbyId);
            if (!addHobby)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetUserHobbyAsync), new { userId, hobbyId });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
        }
    }

    [HttpDelete("{userId}/delete/{hobbyId}")]
    public async Task<IActionResult> RemoveHobbyFromUserAsync(long userId, long hobbyId)
    {
        try
        {
            var removeHobby = await _userService.RemoveHobbyFromUserAsync(userId, hobbyId);
            if (!removeHobby)
            {
                return NotFound();
            }
            return Ok("The hobby has been removed successfully");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
        }
    }
}