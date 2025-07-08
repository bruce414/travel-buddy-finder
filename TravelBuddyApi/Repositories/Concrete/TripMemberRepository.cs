using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Concrete;

public class TripMemberRepository(TravelBuddyContext _travelBuddyContext)
{
    public async Task<List<TripMember>> GetAllTripMembersAsync(long tripId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.TripId == tripId)
                .Include(tm => tm.User)
                .ToListAsync();
    }

    public async Task<TripMember?> GetTripAdminAsync(long tripId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.TripId == tripId && tm.TripRole == Role.Creator)
                .Include(tm => tm.User)
                .FirstOrDefaultAsync();
    }

    public async Task<List<Trip>> GetTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId)
                .Select(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task<List<Trip>> GetPastTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.Past)
                .Select(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task<List<Trip>> GetOngoingTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.InProgress)
                .Select(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task<List<Trip>> GetUpcomingTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.Upcoming)
                .Select(tm => tm.Trip)
                .ToListAsync();
    }
}