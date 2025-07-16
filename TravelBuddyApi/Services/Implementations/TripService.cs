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

    public async Task<TripResponseDTO> GetTripById(long tripId)
    {
        var getTrip = await _tripRepository.GetTripByIdAsync(tripId);

        if (getTrip == null)
        {
            throw new InvalidOperationException("The trip is not found"); 
        }

        TripResponseDTO trip = new TripResponseDTO
        {
            TripId = getTrip.TripId,
            Title = getTrip.Destination,
            Destination = getTrip.Destination,
            StartDate = getTrip.StartDate,
            EndDate = getTrip.EndDate,
            AveragePricePerPerson = getTrip.AveragePricePerPerson,
            Description = getTrip.Description,
            TripImagesUrl = getTrip.TripImagesUrl,
            IsLookingForBuddies = getTrip.IsLookingForBuddies
        };
        return trip;
    }

    public async Task<IEnumerable<TripResponseDTO>> GetFilteredTripsAsync(TripFilter tripFilter)
    {
        var getTrips = await _tripRepository.GetFilteredTripsAsync(tripFilter);

        var result = getTrips.Select(t => new TripResponseDTO
        {
            TripId = t.TripId,
            TripOrganizerId = t.TripOrganizerId,
            Title = t.Title,
            Destination = t.Destination,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            AveragePricePerPerson = t.AveragePricePerPerson,
            Description = t.Description,
            TripImagesUrl = t.TripImagesUrl,
            IsLookingForBuddies = t.IsLookingForBuddies
        });
        return result;
    }

    public async Task<TripResponseDTO> CreateTripAsync(long userId, TripCreateDTO tripCreateDTO)
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

        return new TripResponseDTO
        {
            TripId = newTrip.TripId,
            Title = newTrip.Title,
            Destination = newTrip.Description,
            StartDate = newTrip.StartDate,
            EndDate = newTrip.EndDate,
            AveragePricePerPerson = newTrip.AveragePricePerPerson,
            Description = newTrip.Description,
            TripImagesUrl = newTrip.TripImagesUrl,
            IsLookingForBuddies = tripCreateDTO.IsLookingForBuddies
        };
    }

    public async Task<TripResponseDTO> UpdateTripAsync(long userId, long tripId, TripUpdateDTO tripUpdateDTO)
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

        return new TripResponseDTO
        {
            TripId = existingTrip.TripId,
            TripOrganizerId = existingTrip.TripOrganizerId,
            Title = existingTrip.Title,
            Destination = existingTrip.Destination,
            StartDate = existingTrip.StartDate,
            EndDate = existingTrip.EndDate,
            AveragePricePerPerson = existingTrip.AveragePricePerPerson,
            Description = existingTrip.Description,
            TripImagesUrl = existingTrip.TripImagesUrl,
            IsLookingForBuddies = existingTrip.IsLookingForBuddies
        };
    }

    public async Task<bool> DeleteTripAsync(long userId, long tripId)
    {
        Trip trip = await _tripRepository.GetTripByIdAsync(tripId);
        if (trip == null)
        {
            throw new InvalidOperationException("The requested trip does not exist");
        }
        //Only trip creator can delete a trip
        if (trip.TripOrganizerId != userId)
        {
            throw new UnauthorizedAccessException("Only the trip admin can delete this trip");
        }
        await _tripRepository.RemoveTripByIdAsync(tripId);
        return true;
    }

    public async Task<IEnumerable<TripResponseDTO>> GetUserAllUpcomingTripsAsync(long userId)
    {
        var getTrips = await _tripRepository.GetUserUpcomingTripsAsync(userId);

        var result = getTrips.Select(t => new TripResponseDTO
        {
            TripId = t.TripId,
            TripOrganizerId = t.TripOrganizerId,
            Title = t.Title,
            Destination = t.Destination,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            AveragePricePerPerson = t.AveragePricePerPerson,
            Description = t.Description,
            TripImagesUrl = t.TripImagesUrl,
            IsLookingForBuddies = t.IsLookingForBuddies
        });
        return result;
    }

    public async Task<IEnumerable<TripResponseDTO>> GetUserAllInProgressTripsAsync(long userId)
    {
        var getTrips = await _tripRepository.GetUserInProgressTripsAsync(userId);

        var result = getTrips.Select(t => new TripResponseDTO
        {
            TripId = t.TripId,
            TripOrganizerId = t.TripOrganizerId,
            Title = t.Title,
            Destination = t.Destination,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            AveragePricePerPerson = t.AveragePricePerPerson,
            Description = t.Description,
            TripImagesUrl = t.TripImagesUrl,
            IsLookingForBuddies = t.IsLookingForBuddies
        });
        return result;
    }

    public async Task<IEnumerable<TripResponseDTO>> GetUserAllPastTripsAsync(long userId)
    {
        var getTrips = await _tripRepository.GetUserPastTripsAsync(userId);

        var result = getTrips.Select(t => new TripResponseDTO
        {
            TripId = t.TripId,
            TripOrganizerId = t.TripOrganizerId,
            Title = t.Title,
            Destination = t.Destination,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            AveragePricePerPerson = t.AveragePricePerPerson,
            Description = t.Description,
            TripImagesUrl = t.TripImagesUrl,
            IsLookingForBuddies = t.IsLookingForBuddies
        });
        return result;
    }

    public async Task<IEnumerable<TripResponseDTO>> GetUserAllCancelledTripsAsync(long userId)
    {
        var getTrips = await _tripRepository.GetUserCancelledTripsAsync(userId);

        var result = getTrips.Select(t => new TripResponseDTO
        {
            TripId = t.TripId,
            TripOrganizerId = t.TripOrganizerId,
            Title = t.Title,
            Destination = t.Destination,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            AveragePricePerPerson = t.AveragePricePerPerson,
            Description = t.Description,
            TripImagesUrl = t.TripImagesUrl,
            IsLookingForBuddies = t.IsLookingForBuddies
        });
        return result;
    }
}