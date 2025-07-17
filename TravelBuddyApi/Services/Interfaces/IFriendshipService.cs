using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface IFriendshipService
{
    Task<FriendshipResponseDTO> SendFriendRequestAsync(long fromUserId, long toUserId);
    Task<FriendshipResponseDTO> AcceptFriendRequestAsync(long userId, long requesterId);
    Task<FriendshipResponseDTO> RejectFriendRequestAsync(long userId, long requesterId);
    Task<bool> RemoveFriendAsync(long userId, long friendId);
    Task<FriendshipResponseDTO> GetFriendAsync(long userId, long friendId);
    Task<IEnumerable<FriendshipResponseDTO>> GetFriendsByUserIdAsync(long userId);
    Task<IEnumerable<FriendshipResponseDTO>> GetPendingSentRequestsAsync(long userId);
    Task<IEnumerable<FriendshipResponseDTO>> GetPendingReceivedRequestsAsync(long userId);
    Task<string> GetFriendshipStatusAsync(long userId, long targetUserId);
}