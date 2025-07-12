using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;

namespace TravelBuddyApi.Services.Implementations;

public class FriendshipService(IFriendshipRepository _friendshipRepository, IUserRepository _userRepository)
{
    public async Task<IEnumerable<FriendshipResponseDTO>> GetFriendsByUserId(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        var getFriends = await _friendshipRepository.GetFriendsByUserIdAsync(userId);
        var result = getFriends.Select(f => new FriendshipResponseDTO
        {
            UserId = f.UserId,
            User = f.User,
            FriendId = f.FriendId,
            Friend = f.Friend,
            RequestedAt = f.RequestedAt,
            BecameAt = f.BecameAt,
            FriendshipStatus = f.FriendshipStatus
        });
        return result;
    }

    public async Task<IEnumerable<FriendshipResponseDTO>> GetPendingSentRequestsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        var pendingSentRequest = await _friendshipRepository.GetPendingSentRequestsAsync(userId);
        var result = pendingSentRequest.Select(f => new FriendshipResponseDTO
        {
            UserId = f.UserId,
            User = f.User,
            FriendId = f.FriendId,
            Friend = f.Friend,
            RequestedAt = f.RequestedAt,
            BecameAt = f.BecameAt,
            FriendshipStatus = f.FriendshipStatus
        });
        return result;
    }

    public async Task<IEnumerable<FriendshipResponseDTO>> GetPendingReceivedRequestsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        var pendingReceivedRequest = await _friendshipRepository.GetPendingReceivedRequestsAsync(userId);
        var result = pendingReceivedRequest.Select(f => new FriendshipResponseDTO
        {
            UserId = f.UserId,
            User = f.User,
            FriendId = f.FriendId,
            Friend = f.Friend,
            RequestedAt = f.RequestedAt,
            BecameAt = f.BecameAt,
            FriendshipStatus = f.FriendshipStatus
        });
        return result;
    }

    public async Task SendFriendRequestAsync(long fromUserId, long toUserId)
    {
        var getCurrentUser = await _userRepository.GetUserByIdAsync(fromUserId);
        var getTargetUser = await _userRepository.GetUserByIdAsync(toUserId);
        if (getCurrentUser == null || getTargetUser == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        var getFriendship = _friendshipRepository.GetFriendshipAsync(fromUserId, toUserId);
        if (getFriendship == null)
        {
            throw new InvalidOperationException("The friendship does not exist");
        }

        Friendship newFriendship = new Friendship
        {
            UserId = fromUserId,
            FriendId = toUserId,
            RequestedAt = DateTime.Now,
            FriendshipStatus = FriendshipStatus.Pending
        };

        await _friendshipRepository.AddFriendRequestAsync(newFriendship);
    }

    public async Task AcceptFriendRequestAsync(long userId, long requesterId)
    {
        var getCurrentUser = await _userRepository.GetUserByIdAsync(userId);
        var getRequester = await _userRepository.GetUserByIdAsync(requesterId);
        if (getCurrentUser == null || getRequester == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        var getFriendship = await _friendshipRepository.GetFriendshipAsync(userId, requesterId);
        if (getFriendship == null)
        {
            throw new InvalidOperationException("Friendship does not exist");
        }
        if (getFriendship.FriendshipStatus != FriendshipStatus.Pending)
        {
            throw new InvalidOperationException("The request is invalid");
        }
        getFriendship.FriendshipStatus = FriendshipStatus.Accepted;
        getFriendship.BecameAt = DateTime.Now;
        await _friendshipRepository.UpdateFriendshipAsync(getFriendship);
    }

    public async Task RejectFriendRequestAsync(long userId, long requesterId)
    {
        var getCurrentUser = await _userRepository.GetUserByIdAsync(userId);
        var getRequester = await _userRepository.GetUserByIdAsync(requesterId);
        if (getCurrentUser == null || getRequester == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        var getFriendship = await _friendshipRepository.GetFriendshipAsync(userId, requesterId);
        if (getFriendship == null)
        {
            throw new InvalidOperationException("Friendship does not exist");
        }
        if (getFriendship.FriendshipStatus != FriendshipStatus.Pending)
        {
            throw new InvalidOperationException("The request is invalid");
        }
        getFriendship.FriendshipStatus = FriendshipStatus.Declined;
        getFriendship.BecameAt = DateTime.Now;
        await _friendshipRepository.UpdateFriendshipAsync(getFriendship);
    }

    public async Task RemoveFriendAsync(long userId, long friendId)
    {
        var getCurrentUser = await _userRepository.GetUserByIdAsync(userId);
        var getFriend = await _userRepository.GetUserByIdAsync(friendId);
        if (getCurrentUser == null || getFriend == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        var getFriendship = await _friendshipRepository.GetFriendshipAsync(userId, friendId);
        if (getFriendship == null)
        {
            throw new InvalidOperationException("The friendship does not exist");
        }
        if (getFriendship.FriendshipStatus != FriendshipStatus.Accepted)
        {
            throw new InvalidOperationException("The friendship was not established in the first place");
        }
        getFriendship.FriendshipStatus = FriendshipStatus.Declined;
        await _friendshipRepository.RemoveFriendAsync(getFriendship);
    }

    public async Task<string> getFriendshipStatus(long userId, long targetUserId)
    {
        var getCurrentUser = await _userRepository.GetUserByIdAsync(userId);
        var getTargetUser = await _userRepository.GetUserByIdAsync(targetUserId);
        if (getCurrentUser == null || getTargetUser == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        var getFriendship = await _friendshipRepository.GetFriendshipAsync(userId, targetUserId);
        if (getFriendship == null)
        {
            throw new InvalidOperationException("The friendship does not exist");
        }
        return getFriendship.FriendshipStatus.ToString();
    }
}
