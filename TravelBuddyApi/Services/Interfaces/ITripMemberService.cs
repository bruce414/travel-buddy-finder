using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface ITripMemberService
{
    Task AddMemberAsync(long userId, long tripId, TripMemberResponseDTO tripMemberResponseDTO);
    Task RemoveMemberAsync(long userId, long tripId);
    Task<IEnumerable<TripMember>> GetTripMembers(long tripId);
    Task<bool> IsUserInTrip(long userId, long tripId);
    Task<IEnumerable<Trip>> GetJoinedUserUpcomingTripsAsync(long userId);
    Task<IEnumerable<Trip>> GetJoinedUserOngoingTripsAsync(long userId);
    Task<IEnumerable<Trip>> GetJoinedUserPastTripsAsync(long userId);
}