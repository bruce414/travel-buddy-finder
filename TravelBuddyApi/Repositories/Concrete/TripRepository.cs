namespace TravelBuddyApi.Repositories.Concrete;

using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;

public class TripRepository(TravelBuddyContext _travelBuddyContext)
{
    public async Task<List<Trip>> GetAllTripsAsync()
    {
        return await _travelBuddyContext.Trips.ToListAsync();
    }

    public async Task<Trip?> GetTripByIdAsync(long id)
    {
        return await _travelBuddyContext.Trips.FindAsync(id);
    }

    public async Task<List<Trip>> GetTripsByDestinationAsync(string destination)
    {
        List<Trip> trips = await _travelBuddyContext.Trips.Where(t => t.Destination == destination).ToListAsync();
        return trips;
    }

    public async Task<List<Trip>> GetTripsByStartDateAsync(DateTime startDate)
    {
        List<Trip> trips = await _travelBuddyContext.Trips.Where(t => t.StartDate == startDate).ToListAsync();
        return trips;
    }

    public async Task<List<Trip>> GetTripsByEndDateAsync(DateTime endDate)
    {
        List<Trip> trips = await _travelBuddyContext.Trips.Where(t => t.EndDate == endDate).ToListAsync();
        return trips;
    }

    public async Task<List<Trip>> GetTripsByBothDatesAsync(DateTime startDate, DateTime endDate)
    {
        List<Trip> trips = await _travelBuddyContext.Trips.Where(t => t.StartDate == startDate && t.EndDate == endDate).ToListAsync();
        return trips;
    }

    public async Task AddTripAsync(Trip trip)
    {
        _travelBuddyContext.Add(trip);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task UpdateTripAsync(Trip trip)
    {
        _travelBuddyContext.Entry(trip).State = EntityState.Modified;
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task RemoveTripByIdAsync(long id)
    {
        Trip? TripToBeRemoved = _travelBuddyContext.Trips.Find(id);
        if (TripToBeRemoved != null)
        {
            _travelBuddyContext.Remove(TripToBeRemoved);
            await _travelBuddyContext.SaveChangesAsync();
        }
    }

    public async Task<bool> TripExistsAsync(long id)
    {
        return await _travelBuddyContext.Trips.AnyAsync(t => t.TripId == id);
    }
}