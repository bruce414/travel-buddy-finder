namespace TravelBuddyApi.Repositories.Abstract;

using TravelBuddyApi.Models;

public interface ITripRepository
{
    Task<List<Trip>> GetAllTripsAsync();
    Task<Trip> GetTripByIdAsync(long id);
    Task<List<Trip>> GetFilteredTripsAsync(TripFilter filter);
    Task AddTripAsync(Trip trip);
    Task UpdateTripAsync(Trip trip);
    Task RemoveTripByIdAsync(long id);
    Task SaveChangesAsync();
    Task<bool> TripExistsAsync(long id);
}

//Create a trip filter class to make fitlering trips easier
public class TripFilter
{

}