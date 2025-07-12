using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface IFriendshipService
{
    Task SendFriendRequestAsync(long fromUserId, long toUserId);
    Task AcceptFriendRequestAsync(long userId, long requesterId);
    Task RejectFriendRequestAsync(long userId, long requesterId);
    Task RemoveFriendAsync(long userId, long friendId);
    Task<IEnumerable<FriendshipResponseDTO>> GetFriendsByUserIdAsync(long userId);
    Task<IEnumerable<FriendshipResponseDTO>> GetPendingSentRequestsAsync(long userId);
    Task<IEnumerable<FriendshipResponseDTO>> GetPendingReceivedRequestsAsync(long userId);
    Task<string> GetFriendshipStatusAsync(long userId, long targetUserId);
}