namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public enum Role {Creator, Member, Pending};

public class TripMember
{
    //FK
    public long UserId { get; set; }
    //Navigation property
    public User User { get; set; } = null!;

    //FK
    public long TripId { get; set; }
    //Navigation property
    public Trip Trip { get; set; } = null!;

    //set the default trip role to pending
    public Role MemberStatus { get; set; } = Role.Pending;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}