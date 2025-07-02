namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public enum Role {Creator, Member, Pending};

public class TripMember
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    public Role TripRole { get; set; } = Role.Pending;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}