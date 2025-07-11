using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Concrete;

namespace TravelBuddyApi.Services.Implementations;

public class TripMemberService(UserRepository _userRepository, TripRepository _tripRepository, TripMemberRepository _tripMemberRepository)
{
    public async Task AddMemberAsync(long userId, long tripId, TripMemberResponseDTO tripMemberResponseDTO)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }

        var getTrip = await _tripRepository.GetTripByIdAsync(tripId);
        if (getTrip == null)
        {
            throw new InvalidOperationException("The requested trip does not exist");
        }

        bool isUserInTrip = await _tripMemberRepository.IsUserInTripAsync(userId, tripId);
        if (!isUserInTrip)
        {
            TripMember newMemberInstance = new TripMember
            {
                UserId = userId,
                User = getUser,
                TripId = tripId,
                Trip = getTrip,
                MemberStatus = tripMemberResponseDTO.MemberStatus,
                JoinedAt = tripMemberResponseDTO.JoinedAt
            };

            await _tripMemberRepository.AddMemberAsync(newMemberInstance);
        }
    }

    public async Task RemoveMemberAsync(long userId, long tripId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }

        var getTrip = await _tripRepository.GetTripByIdAsync(tripId);
        if (getTrip == null)
        {
            throw new InvalidOperationException("The requested trip does not exist");
        }

        bool isUserInTrip = await _tripMemberRepository.IsUserInTripAsync(userId, tripId);
        if (isUserInTrip)
        {
            await _tripMemberRepository.RemoveMemberAsync(userId, tripId);
        }
    }

    public async Task<IEnumerable<TripMember>> GetTripMembersAsync(long tripId)
    {
        var getTrip = await _tripRepository.GetTripByIdAsync(tripId);
        if (getTrip == null)
        {
            throw new InvalidOperationException("The requested trip does not exist");
        }
        return await _tripMemberRepository.GetAllTripMembersAsync(tripId);
    }

    public async Task<bool> IsUserInTrip(long userId, long tripId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }

        var getTrip = await _tripRepository.GetTripByIdAsync(tripId);
        if (getTrip == null)
        {
            throw new InvalidOperationException("The requested trip does not exist");
        }
        return await _tripMemberRepository.IsUserInTripAsync(userId, tripId);
    }

    public async Task<IEnumerable<Trip>> GetJoinedUserUpcomingTripsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }
        return await _tripMemberRepository.GetJoinedUpcomingTripsByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Trip>> GetJoinedUserOngoingTripsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }
        return await _tripMemberRepository.GetJoinedOngoingTripsByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Trip>> GetJoinedUserPastTripsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }
        return await _tripMemberRepository.GetJoinedPastTripsByUserIdAsync(userId);
    }
}