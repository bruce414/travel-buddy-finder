namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public enum FriendshipStatus {Accepted, Declined, Pending}

public class FriendShip
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int FriendId { get; set; }
    public User Friend { get; set; } = null!;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime BecameAt { get; set; } = DateTime.UtcNow;
    public FriendshipStatus FriendShipStatus { get; set; } = FriendshipStatus.Pending;
}