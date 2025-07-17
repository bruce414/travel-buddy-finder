namespace TravelBuddyApi.Controllers;

using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Services.Implementations;
using TravelBuddyApi.Services.Interfaces;

[ApiController]
[Route("[controller]")]
public class FriendshipController(IFriendshipService _friendshipService) : ControllerBase
{
    //Get all friends by UserId
    [HttpGet("{userId}/Friends")]
    public async Task<IActionResult> GetAllFriendsByUserIdAsync(long userId)
    {
        try
        {
            var getAllFriends = await _friendshipService.GetFriendsByUserIdAsync(userId);
            if (getAllFriends == null)
            {
                return NotFound();
            }
            return Ok(getAllFriends);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get that specific friend from the user
    [HttpGet("{userId}/{friendId}")]
    public async Task<IActionResult> GetFriendAsync(long userId, long friendId)
    {
        try
        {
            var getFriend = await _friendshipService.GetFriendAsync(userId, friendId);
            if (getFriend == null)
            {
                return NotFound();
            }
            return Ok(getFriend);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get all pending sent requests by userId
    [HttpGet("{userId}/pendingSentRequest")]
    public async Task<IActionResult> GetPendingSentRequestsAsync(long userId)
    {
        try
        {
            var pendingSentRequests = await _friendshipService.GetPendingSentRequestsAsync(userId);
            if (pendingSentRequests == null)
            {
                return NotFound();
            }
            return Ok(pendingSentRequests);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get all pending received requests by userId (the other user has sent requests to me, but I have not approved it)
    [HttpGet("{userId}/PendingReceivedRequests")]
    public async Task<IActionResult> GetPendingReceivedRequestsAsync(long userId)
    {
        try
        {
            var pendingReceivedRequests = await _friendshipService.GetPendingReceivedRequestsAsync(userId);
            if (pendingReceivedRequests == null)
            {
                return NotFound();
            }
            return Ok(pendingReceivedRequests);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Send a friend request to somebody
    [HttpPost]
    public async Task<IActionResult> SendFriendRequestAsync([FromBody] FriendshipCreateRequestDTO friendshipCreateRequestDTO)
    {
        try
        {
            var addFriendRequest = await _friendshipService.SendFriendRequestAsync(friendshipCreateRequestDTO.UserId, friendshipCreateRequestDTO.RequesterId);
            if (addFriendRequest == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetFriendAsync), new { fromUserId = addFriendRequest.UserId, toUserId = addFriendRequest.FriendId }, addFriendRequest);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Accept a friend request, updates the friendship status to accepted
    [HttpPut("{userId}/Accepts/{requesterId}")]
    public async Task<IActionResult> AcceptFriendRequestAsync(long userId, long requesterId)
    {
        try
        {
            var accepted = await _friendshipService.AcceptFriendRequestAsync(userId, requesterId);
            if (accepted == null)
            {
                return NotFound();
            }
            return Ok(accepted);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Reject a friend request, updates the friendship status to declined
    [HttpPut("{userId}/Declines/{requesterId}")]
    public async Task<IActionResult> RejectFriendRequestAsync(long userId, long requesterId)
    {
        try
        {
            var rejected = await _friendshipService.RejectFriendRequestAsync(userId, requesterId);
            if (rejected == null)
            {
                return NotFound();
            }
            return Ok(rejected);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Remove a friend
    [HttpDelete("{userId}/Removes/{friendId}")]
    public async Task<IActionResult> RemoveFriendshipAsync(long userId, long friendId)
    {
        try
        {
            var didRemove = await _friendshipService.RemoveFriendAsync(userId, friendId);
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
            "An error occurred while updating the user.");
        }
    }
}