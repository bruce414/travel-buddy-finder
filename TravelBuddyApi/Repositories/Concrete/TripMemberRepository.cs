using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;

namespace TravelBuddyApi.Repositories.Concrete;

public class TripMemberRepository : ITripMemberRepository
{
    private readonly TravelBuddyContext _travelBuddyContext;

    public TripMemberRepository(TravelBuddyContext travelBuddyContext)
    {
        _travelBuddyContext = travelBuddyContext;
    }
    
    public async Task<IEnumerable<TripMember>> GetTripMembersAsync(long tripId)
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

    public async Task<IEnumerable<TripMember>> GetAllJoinedTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId)
                .Include(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task<IEnumerable<TripMember>> GetJoinedPastTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.Past)
                .Include(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task<IEnumerable<TripMember>> GetJoinedOngoingTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.InProgress)
                .Include(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task<IEnumerable<TripMember>> GetJoinedUpcomingTripsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.UserId == userId && tm.Trip.TripStatus == TripStatus.Upcoming)
                .Include(tm => tm.Trip)
                .ToListAsync();
    }

    public async Task AddMemberAsync(TripMember tripMember)
    {
        _travelBuddyContext.TripMembers.Add(tripMember);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task UpdateMemberAsync(TripMember tripMember)
    {
        _travelBuddyContext.TripMembers.Update(tripMember);
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

    public async Task<TripMember?> GetTripMemberAsync(long tripId, long userId)
    {
        return await _travelBuddyContext.TripMembers
                .Where(tm => tm.TripId == tripId && tm.UserId == userId)
                .Include(tm => tm.User)
                .FirstOrDefaultAsync();
    }

}
