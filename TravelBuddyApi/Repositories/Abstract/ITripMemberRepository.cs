using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface ITripMemberRepository
{
    //Get all trip members from a trip
    Task<IEnumerable<TripMember>> GetTripMembersAsync(long tripId); 
    //Get the trip member instance based on userId
    Task<TripMember?> GetTripMemberAsync(long tripId, long userId);

    Task<TripMember?> GetTripAdminAsync(long tripId);

    //Retrieve all trips that the user is a part of
    Task<IEnumerable<TripMember>> GetAllJoinedTripsByUserIdAsync(long userId);
    Task<IEnumerable<TripMember>> GetJoinedPastTripsByUserIdAsync(long userId);
    Task<IEnumerable<TripMember>> GetJoinedOngoingTripsByUserIdAsync(long userId);
    Task<IEnumerable<TripMember>> GetJoinedUpcomingTripsByUserIdAsync(long userId);

    Task AddMemberAsync(TripMember tripMember);
    Task UpdateMemberAsync(TripMember tripMember);
    Task RemoveMemberAsync(long userId, long tripId);
    Task<bool> IsUserInTripAsync(long userId, long tripId);
}