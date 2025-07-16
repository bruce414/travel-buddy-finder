using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface ITripMemberService
{
    Task<TripMemberResponseDTO> AddMemberAsync(long userId, long tripId, TripMemberCreateDTO tripMemberCreateDTO);
    Task<TripMemberResponseDTO> UpdateMemberAsync(long userId, long tripid, TripMemberUpdateDTO tripMemberUpdateDTO);
    Task<bool> RemoveMemberAsync(long userId, long tripId);
    Task<IEnumerable<TripMemberResponseDTO>> GetTripMembersAsync(long tripId);
    Task<TripMemberResponseDTO> GetTripMemberAsync(long tripId, long userId);
    Task<bool> IsUserInTripAsync(long userId, long tripId);
    Task<IEnumerable<TripResponseDTO>> GetJoinedUserUpcomingTripsAsync(long userId);
    Task<IEnumerable<TripResponseDTO>> GetJoinedUserOngoingTripsAsync(long userId);
    Task<IEnumerable<TripResponseDTO>> GetJoinedUserPastTripsAsync(long userId);
}