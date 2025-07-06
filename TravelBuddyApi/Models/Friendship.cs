namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public enum FriendshipStatus {Accepted, Declined, Pending}

public class Friendship
{
    //FK
    public long UserId { get; set; }
    //Navigation property
    public User User { get; set; } = null!;

    //FK
    public long FriendId { get; set; }
    //Navigation property
    public User Friend { get; set; } = null!;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime BecameAt { get; set; } = DateTime.UtcNow;
    //set the default friendship status to pending
    public FriendshipStatus FriendshipStatus { get; set; } = FriendshipStatus.Pending;
}