using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface ITripService
{
    public Task<IEnumerable<TripResponseDTO>> GetAllTripsAsync();
    public Task<TripResponseDTO> GetTripById(long tripId);
    public Task<IEnumerable<TripResponseDTO>> GetFilteredTripsAsync(TripFilter tripFilter);
    public Task<TripResponseDTO> CreateTripAsync(long userId, TripCreateDTO tripDTO);
    public Task<TripResponseDTO> UpdateTripAsync(long userId, long tripId, TripUpdateDTO tripDTO);
    public Task<bool> DeleteTripAsync(long userId, long tripid);
    public Task<IEnumerable<TripResponseDTO>> GetUserAllUpcomingTripsAsync(long userId);
    public Task<IEnumerable<TripResponseDTO>> GetUserAllInProgressTripsAsync(long userId);
    public Task<IEnumerable<TripResponseDTO>> GetUserAllPastTripsAsync(long userId);
    public Task<IEnumerable<TripResponseDTO>> GetUserAllCancelledTripsAsync(long userId);
}