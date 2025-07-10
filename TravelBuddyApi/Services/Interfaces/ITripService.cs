using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface ITripService
{
    public Task<IEnumerable<Trip>> GetAllTripsAsync();
    public Task<IEnumerable<Trip>> GetFilteredTripsAsync(TripFilter tripFilter);
    public Task CreateTripsAsync(long userId, TripCreateDTO tripDTO);
    public Task UpdateTripAsync(long userId, long tripId, TripUpdateDTO tripDTO);
    public Task DeleteTripAsync(long userId, long tripid);
    public Task<IEnumerable<Trip>> GetUserAllUpcomingTripsAsync(long userId);
    public Task<IEnumerable<Trip>> GetUserAllInProgressTripsAsync(long userId);
    public Task<IEnumerable<Trip>> GetUserAllPastTripsAsync(long userId);
    public Task<IEnumerable<Trip>> GetUserAllCancelledTripsAsync(long userId);
}