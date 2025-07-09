using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface IFriendshipRepository
{
    Task<List<Friendship>> GetFriendsByUserIdAsync(long userId);
    Task<List<Friendship>> GetFriendsByFriendshipStatusAsync(long userId, FriendshipStatus friendshipStatus);
}