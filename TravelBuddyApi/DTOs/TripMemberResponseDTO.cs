using TravelBuddyApi.Models;

namespace TravelBuddyApi.DTOs;

public class TripMemberResponseDTO
{
    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public long TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    public Role MemberStatus { get; set; } = Role.Pending;
    public DateTime JoinedAt { get; set; }
}