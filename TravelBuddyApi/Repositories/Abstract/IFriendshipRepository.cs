using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface IFriendshipRepository
{
    Task<IEnumerable<Friendship>> GetFriendsByUserIdAsync(long userId);
    Task<Friendship> GetFriendAsync(long userId, long friendId);
    Task<IEnumerable<Friendship>> GetFriendsByFriendshipStatusAsync(long userId, FriendshipStatus friendshipStatus);
    Task<IEnumerable<Friendship>> GetPendingSentRequestsAsync(long userId);
    Task<IEnumerable<Friendship>> GetPendingReceivedRequestsAsync(long friendId);
    Task AddFriendRequestAsync(Friendship friendship);
    Task UpdateFriendshipAsync(Friendship friendship);
    Task RemoveFriendAsync(Friendship friendship);
    // Task SentFriendRequest(long fromUserId, long toUserId);
    Task<Friendship> GetFriendshipAsync(long userId, long targetUserId);
}