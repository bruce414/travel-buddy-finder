namespace TravelBuddyApi.Repositories.Abstract;

using TravelBuddyApi.Models;

public interface ITripRepository
{
    Task<List<Trip>> GetAllTripsAsync();
    Task<Trip?> GetTripByIdAsync(long tripId);
    Task<List<Trip>> GetFilteredTripsAsync(TripFilter filter);

    //Retrieve all Upcoming trips that are !!created!! by the user
    Task<IEnumerable<Trip>> GetUserUpcomingTripsAsync(long userId);
    //Retrieve all Inprogress trips that are !!created!! by the user
    Task<IEnumerable<Trip>> GetUserInProgressTripsAsync(long userId);
    //Retrieve all Past trips that are !!created!! by the user
    Task<IEnumerable<Trip>> GetUserPastTripsAsync(long userId);
      //Retrieve all Cancelled trips that are !!created!! by the user
    Task<IEnumerable<Trip>> GetUserCancelledTripsAsync(long userId);
    
    Task AddTripAsync(Trip trip);
    Task UpdateTripAsync(Trip trip);
    Task RemoveTripByIdAsync(long id);
    Task<int> CurrentNumberOfMembersAsync(int tripId);
    Task<bool> TripExistsAsync(long id);
}