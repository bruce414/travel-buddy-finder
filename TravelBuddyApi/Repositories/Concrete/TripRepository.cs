namespace TravelBuddyApi.Repositories.Concrete;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;

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

    //Filtering trips based on users selections for ***search query***
    public async Task<List<Trip>> GetFilteredTripsAsync(TripFilter tripFilter)
    {
        var tripQuery = _travelBuddyContext.Trips.AsQueryable();

        if (!string.IsNullOrWhiteSpace(tripFilter.Destination))
        {
            tripQuery = tripQuery.Where(t => t.Destination == tripFilter.Destination);
        }

        if (tripFilter.StartDate.HasValue)
        {
            tripQuery = tripQuery.Where(t => t.StartDate == tripFilter.StartDate);
        }

        if (tripFilter.EndDate.HasValue)
        {
            tripQuery = tripQuery.Where(t => t.EndDate == tripFilter.EndDate);
        }

        if (tripFilter.IsActivelylookingForBuddies.HasValue)
        {
            tripQuery = tripQuery.Where(t => t.IsLookingForBuddies == tripFilter.IsActivelylookingForBuddies);
        }

        if (tripFilter.AveragePricePerPerson.HasValue)
        {
            tripQuery = tripQuery.Where(t => t.AveragePricePerPerson == tripFilter.AveragePricePerPerson);
        }

        return await tripQuery.ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetUserUpcomingTripsAsync(long userId)
    {
        return await _travelBuddyContext.Trips
                .Where(t => t.TripOrganizerId == userId && t.TripStatus == TripStatus.Upcoming)
                .Include(t => t.TripOrganizerId)
                .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetUserInProgressTripsAsync(long userId)
    {
        return await _travelBuddyContext.Trips
                .Where(t => t.TripOrganizerId == userId && t.TripStatus == TripStatus.InProgress)
                .Include(t => t.TripOrganizerId)
                .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetUserPastTripsAsync(long userId)
    {
        return await _travelBuddyContext.Trips
                .Where(t => t.TripOrganizerId == userId && t.TripStatus == TripStatus.Past)
                .Include(t => t.TripOrganizerId)
                .ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetUserCancelledTripsAsync(long userId)
    {
        return await _travelBuddyContext.Trips
                .Where(t => t.TripOrganizerId == userId && t.TripStatus == TripStatus.Cancelled)
                .Include(t => t.TripOrganizerId)
                .ToListAsync();
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

    public async Task<int> CurrentNumberOfMembers(int tripId)
    {
        Trip? trip = await _travelBuddyContext.Trips.Include(t => t.Members).FirstOrDefaultAsync(tr => tr.TripId == tripId);
        int memberCount = trip?.Members.Count ?? 0;
        return memberCount;
    }

    public async Task<bool> TripExistsAsync(long id)
    {
        return await _travelBuddyContext.Trips.AnyAsync(t => t.TripId == id);
    }
}