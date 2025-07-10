using Microsoft.AspNetCore.Http.HttpResults;
using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;

namespace TravelBuddyApi.Services.Implementations;

public class TripService(ITripRepository _tripRepository)
{
    public async Task<IEnumerable<Trip>> GetAllTripsAsync()
    {
        return await _tripRepository.GetAllTripsAsync();
    }

    public async Task<IEnumerable<Trip>> GetFilteredTripsAsync(TripFilter tripFilter)
    {
        return await _tripRepository.GetFilteredTripsAsync(tripFilter);
    }

    public async Task CreateTripAsync(long userId, TripCreateDTO tripCreateDTO)
    {
        var newTrip = new Trip
        {
            TripOrganizerId = userId,
            Title = tripCreateDTO.Title,
            Destination = tripCreateDTO.Destination,
            StartDate = tripCreateDTO.StartDate,
            EndDate = tripCreateDTO.EndDate,
            AveragePricePerPerson = tripCreateDTO.AveragePricePerPerson,
            Description = tripCreateDTO.Description,
            TripImagesUrl = tripCreateDTO.TripImagesUrl,
            IsLookingForBuddies = tripCreateDTO.IsLookingForBuddies
        };

        await _tripRepository.AddTripAsync(newTrip);
    }

    public async Task UpdateTripAsync(long userId, long tripId, TripUpdateDTO tripUpdateDTO)
    {
        Trip existingTrip = await _tripRepository.GetTripByIdAsync(tripId);
        if (existingTrip == null)
        {
            throw new InvalidOperationException("The requested trip does not exist");
        }
        if (existingTrip.TripOrganizerId != userId)
        {
            throw new UnauthorizedAccessException("Only the trip admin can delete this trip");
        }
        
        existingTrip.TripOrganizerId = userId;
        existingTrip.Title = tripUpdateDTO.Title;
        existingTrip.Destination = tripUpdateDTO.Destination;
        existingTrip.StartDate = tripUpdateDTO.StartDate;
        existingTrip.EndDate = tripUpdateDTO.EndDate;
        existingTrip.AveragePricePerPerson = tripUpdateDTO.AveragePricePerPerson;
        existingTrip.Description = tripUpdateDTO.Description;
        existingTrip.TripImagesUrl = tripUpdateDTO.TripImagesUrl;
        existingTrip.IsLookingForBuddies = tripUpdateDTO.IsLookingForBuddies;

        await _tripRepository.UpdateTripAsync(existingTrip);
    }

    public async Task DeleteTripAsync(long userId, long tripId)
    {
        Trip trip = await _tripRepository.GetTripByIdAsync(tripId);
        if (trip == null)
        {
            throw new InvalidOperationException("The requested trip does not exist");
        }
        if (trip.TripOrganizerId != userId)
        {
            throw new UnauthorizedAccessException("Only the trip admin can delete this trip");
        }
        await _tripRepository.RemoveTripByIdAsync(tripId);
    }

    public async Task<IEnumerable<Trip>> GetUserAllUpcomingTripsAsync(long userId)
    {
        return await _tripRepository.GetUserUpcomingTripsAsync(userId);
    }

    public async Task<IEnumerable<Trip>> GetUserAllInProgressTripsAsync(long userId)
    {
        return await _tripRepository.GetUserInProgressTripsAsync(userId);
    }

    public async Task<IEnumerable<Trip>> GetUserAllPastTripsAsync(long userId)
    {
        return await _tripRepository.GetUserPastTripsAsync(userId);
    }

    public async Task<IEnumerable<Trip>> GetUserAllCancelledTripsAsync(long userId)
    {
        return await _tripRepository.GetUserCancelledTripsAsync(userId);
    }
}