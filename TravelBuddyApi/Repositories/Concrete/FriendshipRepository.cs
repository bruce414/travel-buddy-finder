using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Concrete;

public class FriendshipRepository(TravelBuddyContext _travelBuddyContext)
{
    public async Task<IEnumerable<Friendship>> GetFriendsByUserIdAsync(long userId)
    {
        return await _travelBuddyContext.Friendships
                .Where(u => u.UserId == userId && u.FriendshipStatus == FriendshipStatus.Accepted)
                .Include(u => u.Friend)
                .ToListAsync();
    }
    
    public async Task<IEnumerable<Friendship>> GetFriendsByFrindshipStatusAsync(long userId)
    {
        return await _travelBuddyContext.Friendships
                .Where(f => f.UserId == userId && f.FriendshipStatus == FriendshipStatus.Pending)
                .Include(f => f.Friend)
                .ToListAsync();
    }

    //Logic: I am the one who is sending the friend requests, therefore, DB needs to look for all rows in Friendships 
    //where the userId is me!!!
    public async Task<IEnumerable<Friendship>> GetPendingSentRequestsAsync(long userId)
    {
        return await _travelBuddyContext.Friendships
                .Where(f => f.UserId == userId && f.FriendshipStatus == FriendshipStatus.Pending)
                .Include(f => f.Friend)
                .ToListAsync();
    }

    //Logic, I'm the one who is receving all these friend requests, therefore, I'm the friend they (other users) are 
    //requesting, thus, needs to look for all rows in the Friendship where FriendId is me!!!
    public async Task<IEnumerable<Friendship>> GetPendingReceivedRequestsAsync(long userId)
    {
        return await _travelBuddyContext.Friendships
                .Where(f => f.FriendId == userId && f.FriendshipStatus == FriendshipStatus.Pending)
                .Include(f => f.User)
                .ToListAsync();
    }

    public async Task AddFriendRequestAsync(Friendship friendship)
    {
        _travelBuddyContext.Friendships.Add(friendship);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task UpdateFriendshipAsync(Friendship friendship)
    {
        _travelBuddyContext.Friendships.Update(friendship);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task RemoveFriendAsync(Friendship friendship)
    {
        _travelBuddyContext.Friendships.Remove(friendship);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task<Friendship?> GetFriendshipAsync(long userId, long targetUserId)
    {
        return await _travelBuddyContext.Friendships
                .Where(f => f.UserId == userId && f.FriendId == targetUserId)
                .FirstOrDefaultAsync();
    }
}