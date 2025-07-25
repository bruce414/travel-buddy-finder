using TravelBuddyApi.Models;

namespace TravelBuddyApi.DTOs;

public class TripMemberResponseDTO
{
    public long UserId { get; set; }
    public UserResponseDTO User { get; set; } = null!;

    public long TripId { get; set; }
    public TripResponseDTO Trip { get; set; } = null!;

    public Role MemberStatus { get; set; } = Role.Pending;
    public DateTime JoinedAt { get; set; }
}