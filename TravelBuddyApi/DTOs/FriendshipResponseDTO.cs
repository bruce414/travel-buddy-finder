using TravelBuddyApi.Models;

namespace TravelBuddyApi.DTOs;

public class FriendshipResponseDTO
{
    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public long FriendId { get; set; }
    public User Friend { get; set; } = null!;

    public DateTime RequestedAt { get; set; }
    public DateTime BecameAt { get; set; }
    public FriendshipStatus FriendshipStatus { get; set; } = FriendshipStatus.Pending;
}