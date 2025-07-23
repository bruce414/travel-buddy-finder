namespace TravelBuddyApi.Controllers;

using TravelBuddyApi.Services.Interfaces;
using TravelBuddyApi.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HobbyController(IHobbyService _hobbyService) : ControllerBase
{
    //Get all hobbies
    [HttpGet("gethobby")]
    public async Task<IActionResult> GetAllHobbiesAsync()
    {
        try
        {
            var getAllHobbies = await _hobbyService.GetAllHobbiesAsync();
            if (getAllHobbies == null)
            {
                return NotFound();
            }
            return Ok(getAllHobbies);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
        }
    }

    //Get User hobbies
    [HttpGet("{userId}/Hobbies")]
    public async Task<IActionResult> GetUserHobbiesAsync(long userId)
    {
        try
        {
            var userHobbies = await _hobbyService.GetUserHobbiesAsync(userId);
            if (userHobbies == null)
            {
                return NotFound();
            }
            return Ok(userHobbies);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
        }
    }

    // [HttpPost("{userId}/add-hobby")]
    // public async Task<IActionResult> UserAddHobbyAsync(long userId, [FromBody] HobbyCreateDTO hobbyCreateDTO)
    // {
    //     try
    //     {
    //         var addHobby = await _hobbyService.AddHobbyToUserAsync(userId, hobbyCreateDTO);
    //         if (addHobby == null)
    //         {
    //             return NotFound();
    //         }
    //         return CreatedAtAction(nameof(GetUserHobbyAsync), new { userId, hobbyId = hobbyCreateDTO.HobbyId }, addHobby);
    //     }
    //     catch (Exception)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
    //     }
    // }

    // [HttpDelete("Remove-hobby/{hobbyId}/from/{userId}")]
    // public async Task<IActionResult> RemoveHobbyFromTheUserAsync(long userId, long hobbyId)
    // {
    //     try
    //     {
    //         var didRemove = await _hobbyService.RemoveHobbyFromUserAsync(userId, hobbyId);
    //         if (!didRemove)
    //         {
    //             return NotFound();
    //         }
    //         return NoContent();
    //     }
    //     catch (Exception)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
    //     }
    // }
}