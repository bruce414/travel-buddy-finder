namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public enum Role {Creator, Member, Pending};
public enum TripStatus {Upcoming, InProgress, Past, Cancelled};

public class TripMember
{
    //FK
    public int UserId { get; set; }
    //Navigation property
    public User User { get; set; } = null!;

    //FK
    public int TripId { get; set; }
    //Navigation property
    public Trip Trip { get; set; } = null!;

    //set the default trip role to pending
    public Role TripRole { get; set; } = Role.Pending;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public TripStatus TripStatus { get; set; } = TripStatus.Upcoming;
}