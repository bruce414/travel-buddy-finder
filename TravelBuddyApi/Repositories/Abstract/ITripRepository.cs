namespace TravelBuddyApi.Repositories.Abstract;

using TravelBuddyApi.Models;

public interface ITripRepository
{
    Task<List<Trip>> GetAllTripsAsync();
    Task<Trip> GetTripByIdAsync(long id);
    Task<List<Trip>> GetFilteredTripsAsync(TripFilter filter);
    Task<IEnumerable<Trip>> GetUserUpcomingTripsAsync(long userId);
    Task<IEnumerable<Trip>> GetUserInProgressTripsAsync(long userId);
    Task<IEnumerable<Trip>> GetUserPastTripsAsync(long userId);
    Task<IEnumerable<Trip>> GetUserCancelledTripsAsync(long userId);
    Task AddTripAsync(Trip trip);
    Task UpdateTripAsync(Trip trip);
    Task RemoveTripByIdAsync(long id);
    Task<int> CurrentNumberOfMembersAsync(int tripId);
    Task SaveChangesAsync();
    Task<bool> TripExistsAsync(long id);
}