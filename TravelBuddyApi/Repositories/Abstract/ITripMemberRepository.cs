using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface ITripMemberRepository
{
    Task<IList<TripMember>> GetAllTripMembersAsync(long tripId);
    
    Task<TripMember> GetTripAdminAsync(long tripId);
    Task<List<Trip>> GetTripsByUserIdAsync(long userId);
    Task<List<Trip>> GetJoinedPastTripsByUserIdAsync(long userId);
    Task<List<Trip>> GetJoinedOngoingTripsByUserIdAsync(long userId);
    Task<List<Trip>> GetJoinedUpcomingTripsByUserIdAsync(long userId);
    
    Task AddMemberAsync(TripMember tripMember);
    Task RemoveMemberAsync(long userId, long tripId);
    Task<bool> IsUserInTripAsync(long userId, long tripId);
    Task<IEnumerable<TripMember>> GetTripMembersAsync();
}