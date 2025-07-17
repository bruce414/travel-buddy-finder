namespace TravelBuddyApi.DTOs;

using TravelBuddyApi.Models;

public class FriendshipCreateRequestDTO
{
    public long UserId { get; set; }
    public long RequesterId { get; set; }
    public DateTime RequestedAt { get; set; }
    public FriendshipStatus FriendshipStatus { get; set; } = FriendshipStatus.Pending;
}