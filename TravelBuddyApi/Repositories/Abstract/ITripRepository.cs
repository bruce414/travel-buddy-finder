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
    Task<int> CurrentNumberOfMembersAsync(int tripId);
    Task SaveChangesAsync();
    Task<bool> TripExistsAsync(long id);
}

//Create a trip filter class to make fitlering trips easier
public class TripFilter
{
    public string? Destination { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsActivelylookingForBuddies { get; set; }
    public float? AveragePricePerPerson { get; set; }
}