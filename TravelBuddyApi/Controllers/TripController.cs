namespace TravelBuddyApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TripController(ITripService _tripService) : ControllerBase
{
    //Get All Actions
    [HttpGet("trips")]
    public async Task<IActionResult> GetAllTripsAsync()
    {
        try
        {
            var getTrips = await _tripService.GetAllTripsAsync();
            return Ok(getTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get Trip By Id
    [HttpGet("{tripId}")]
    public async Task<IActionResult> GetTripByIdAsync(long tripId)
    {
        try
        {
            var getTrip = await _tripService.GetTripById(tripId);
            return Ok(getTrip);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpGet("{userId}/UpcomingTrips")]
    public async Task<IActionResult> GetUserAllUpcomingTripsAsync(long userId)
    {
        try
        {
            var getTrips = await _tripService.GetUserAllUpcomingTripsAsync(userId);
            if (getTrips == null)
            {
                return NotFound();
            }

            if (!getTrips.Any())
            {
                return Ok(getTrips);
            }

            return Ok(getTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpGet("{userId}/InProgressTrips")]
    public async Task<IActionResult> GetUserAllInProgressTripsAsync(long userId)
    {
        try
        {
            var getTrips = await _tripService.GetUserAllInProgressTripsAsync(userId);
            if (getTrips == null)
            {
                return NotFound();
            }

            if (!getTrips.Any())
            {
                return Ok(getTrips);
            }

            return Ok(getTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpGet("{userId}/PastTrips")]
    public async Task<IActionResult> GetUserAllPastTripsAsync(long userId)
    {
        try
        {
            var getTrips = await _tripService.GetUserAllPastTripsAsync(userId);
            if (getTrips == null)
            {
                return NotFound();
            }
            return Ok(getTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpGet("{userId}/CancelledTrips")]
    public async Task<IActionResult> GetUserAllCancelledTripsAsync(long userId)
    {
        try
        {
            var getTrips = await _tripService.GetUserAllCancelledTripsAsync(userId);
            if (getTrips == null)
            {
                return NotFound();
            }
            return Ok(getTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredTripsAsync([FromQuery] TripFilter tripFilter)
    {
        if (tripFilter == null)
        {
            return BadRequest("The filtering conditions are required");
        }

        try
        {
            var getFilteredTrips = await _tripService.GetFilteredTripsAsync(tripFilter);
            if (!getFilteredTrips.Any())
            {
                return NotFound("Sorry, we cannot find any trips based on your filter option(s)");
            }
            return Ok(getFilteredTrips);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Post: api/Trip
    [HttpPost("addtrips")]
    public async Task<IActionResult> AddTripAsync(long userId, [FromBody] TripCreateDTO tripCreateDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var createdTrip = await _tripService.CreateTripAsync(userId, tripCreateDTO);
            if (createdTrip == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetTripByIdAsync), new { tripId = createdTrip.TripId }, createdTrip);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpPut("{tripId}")]
    public async Task<IActionResult> UpdateTripAsync(long userId, long tripId, [FromBody] TripUpdateDTO tripUpdateDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var updatedResponse = await _tripService.UpdateTripAsync(userId, tripId, tripUpdateDTO);
            if (updatedResponse == null)
            {
                return NotFound();
            }
            return Ok(updatedResponse);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpDelete("tripId")]
    public async Task<IActionResult> RemoveTripAsync(long userId, long tripId)
    {
        try
        {
            var didRemove = await _tripService.DeleteTripAsync(userId, tripId);
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