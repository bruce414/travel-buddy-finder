using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface ITripMemberRepository
{
    Task<IList<TripMember>> GetAllTripMembersAsync(long tripId);
    Task<TripMember> GetTripAdminAsync(long tripId);
    Task<List<Trip>> GetTripsByUserIdAsync(long userId);
    Task<List<Trip>> GetPastTripsByUserIdAsync(long userId);
    Task<List<Trip>> GetOngoingTripsByUserIdAsync(long userId);
    Task<List<Trip>> GetUpcomingTripsByUserIdAsync(long userId);
}