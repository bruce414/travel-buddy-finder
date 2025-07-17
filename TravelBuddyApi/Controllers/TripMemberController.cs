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
public class TripMemberController(ITripMemberService _tripMemberService) : ControllerBase
{
    //Get All TripMember from a selected Trip
    [HttpGet("{tripId}")]
    public async Task<IActionResult> GetAllTripMemberAsync(long tripId)
    {
        try
        {
            var getTripMember = await _tripMemberService.GetTripMembersAsync(tripId);
            if (getTripMember == null)
            {
                return NotFound();
            }
            return Ok(getTripMember);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpGet("{tripId}/members/{userId}")]
    public async Task<IActionResult> GetTripMemberAsync(long tripId, long userId)
    {
        try
        {
            var getTripMember = await _tripMemberService.GetTripMemberAsync(tripId, userId);
            if (getTripMember == null)
            {
                return NotFound();
            }
            return Ok(getTripMember);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get all trips that the trip member is going to go (Upcoming trip)
    [HttpGet("{userId}/GetAllUpcomingTrips")]
    public async Task<IActionResult> GetUserJoinedAllUpcomingTripsAsync(long userId)
    {
        try
        {
            var getAllUpcomingTrips = await _tripMemberService.GetJoinedUserUpcomingTripsAsync(userId);
            if (getAllUpcomingTrips == null)
            {
                return NotFound();
            }
            return Ok(getAllUpcomingTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get all trips that the trip member is currently on (Ongoing trips)
    [HttpGet("{userId}/GetAllOngoingTrips")]
    public async Task<IActionResult> GetUserJoinedAllOngoingTripsAsync(long userId)
    {
        try
        {
            var getAllOngoingTrips = await _tripMemberService.GetJoinedUserOngoingTripsAsync(userId);
            if (getAllOngoingTrips == null)
            {
                return NotFound();
            }
            return Ok(getAllOngoingTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get all trips that the trip member has participated (Past trips)
    [HttpGet("{userId}/GetAllPastTrips")]
    public async Task<IActionResult> GetUserJoinedAllPastTripsAsync(long userId)
    {
        try
        {
            var getAllPastTrips = await _tripMemberService.GetJoinedUserPastTripsAsync(userId);
            if (getAllPastTrips == null)
            {
                return NotFound();
            }
            return Ok(getAllPastTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Add member
    [HttpPost]
    public async Task<IActionResult> AddTripMemberAsync(long userId, long tripId, [FromBody] TripMemberCreateDTO tripMemberCreateDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var addTrip = await _tripMemberService.AddMemberAsync(userId, tripId, tripMemberCreateDTO);
            if (addTrip == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetTripMemberAsync), new { tripId = addTrip.TripId, userId = addTrip.UserId }, addTrip);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateTripMemberAsync(long userId, long tripId, [FromBody] TripMemberUpdateDTO tripMemberUpdateDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var updatedTripMember = await _tripMemberService.UpdateMemberAsync(userId, tripId, tripMemberUpdateDTO);
            if (updatedTripMember == null)
            {
                return NotFound();
            }
            return Ok(updatedTripMember);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }

    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveTripMemberAsync(long userId, long tripId)
    {
        try
        {
            var didRemove = await _tripMemberService.RemoveMemberAsync(userId, tripId);
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
}