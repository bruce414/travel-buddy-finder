using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.DTOs;
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
                .Where(tm => tm.TripId == tripId && tm.MemberStatus == Role.Creator)
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

    public async Task<List<Trip>> GetJoinedPastTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.Past)
                .Include(tm => tm.Trip)
                .Select(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task<List<Trip>> GetJoinedOngoingTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.InProgress)
                .Include(tm => tm.Trip)
                .Select(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task<List<Trip>> GetJoinedUpcomingTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.Upcoming)
                .Include(tm => tm.Trip)
                .Select(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task AddMemberAsync(TripMember tripMember)
    {
        _travelBuddyContext.TripMembers.Add(tripMember);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task RemoveMemberAsync(long userId, long tripId)
    {
        var getMember = await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.TripId == tripId)
                .FirstOrDefaultAsync();

        if (getMember != null)
        {
            _travelBuddyContext.TripMembers.Remove(getMember);
            await _travelBuddyContext.SaveChangesAsync();
        }

    }

    public async Task<bool> IsUserInTripAsync(long userId, long tripId)
    {
        return await _travelBuddyContext.TripMembers
                .AnyAsync(tm => tm.TripId == tripId && tm.UserId == userId);
    }

    public async Task<IEnumerable<TripMember>> GetTripMembersAsync(long tripId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.TripId == tripId)
                .Include(t => t.User)
                .ToListAsync();
    }
        
}
