using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Concrete;

public class FriendshipRepository(TravelBuddyContext _travelBuddyContext)
{
    public async Task<List<Friendship>> GetFriendsByFrindshipStatusAsync(long userId, FriendshipStatus friendshipStatus)
    {
        return await _travelBuddyContext.Friendships
                .Where(f => f.UserId == userId && f.FriendshipStatus == friendshipStatus)
                .Include(f => f.Friend)
                .ToListAsync();
    }
}