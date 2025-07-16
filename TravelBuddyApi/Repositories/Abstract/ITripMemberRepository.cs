using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface ITripMemberRepository
{
    Task<IEnumerable<TripMember>> GetAllTripMembersAsync(long tripId);

    Task<TripMember> GetTripAdminAsync(long tripId);
    Task<IEnumerable<Trip>> GetTripsByUserIdAsync(long userId);
    Task<IEnumerable<Trip>> GetJoinedPastTripsByUserIdAsync(long userId);
    Task<IEnumerable<Trip>> GetJoinedOngoingTripsByUserIdAsync(long userId);
    Task<IEnumerable<Trip>> GetJoinedUpcomingTripsByUserIdAsync(long userId);

    Task AddMemberAsync(TripMember tripMember);
    Task UpdateMemberAsync(TripMember tripMember);
    Task RemoveMemberAsync(long userId, long tripId);
    Task<bool> IsUserInTripAsync(long userId, long tripId);
    Task<IEnumerable<TripMember>> GetTripMembersAsync(long tripId);
    Task<TripMember> GetTripMemberAsync(long tripId, long userId);
}