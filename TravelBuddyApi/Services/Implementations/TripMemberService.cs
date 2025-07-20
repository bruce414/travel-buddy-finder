using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;
using TravelBuddyApi.Repositories.Concrete;
using TravelBuddyApi.Services.Interfaces;

namespace TravelBuddyApi.Services.Implementations;

public class TripMemberService : ITripMemberService
{
    private readonly IUserRepository _userRepository;
    private readonly ITripRepository _tripRepository;
    private readonly ITripMemberRepository _tripMemberRepository;

    public TripMemberService(IUserRepository userRepository, ITripRepository tripRepository, ITripMemberRepository tripMemberRepository)
    {
        _userRepository = userRepository;
        _tripRepository = tripRepository;
        _tripMemberRepository = tripMemberRepository;
    }

    public async Task<TripMemberResponseDTO> AddMemberAsync(long userId, long tripId, TripMemberCreateDTO tripMemberCreateDTO)
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
            throw new InvalidOperationException("User already in Trip, no need to be added again");
        }
        TripMember newMemberInstance = new TripMember
        {
            UserId = userId,
            User = getUser,
            TripId = tripId,
            Trip = getTrip,
            MemberStatus = tripMemberCreateDTO.MemberStatus,
            JoinedAt = tripMemberCreateDTO.JoinedAt
        };

        await _tripMemberRepository.AddMemberAsync(newMemberInstance);
        return new TripMemberResponseDTO
        {
            UserId = newMemberInstance.UserId,
            TripId = newMemberInstance.TripId,
            MemberStatus = newMemberInstance.MemberStatus,
            JoinedAt = newMemberInstance.JoinedAt
        };
    }

    public async Task<TripMemberResponseDTO> UpdateMemberAsync(long userId, long tripId, TripMemberUpdateDTO tripMemberUpdateDTO)
    {
        var getTripMember = await _tripMemberRepository.GetTripMemberAsync(userId, tripId);
        if (getTripMember == null)
        {
            throw new InvalidOperationException("The user is not in this trip");
        }

        getTripMember.MemberStatus = tripMemberUpdateDTO.MemberStatus;

        await _tripMemberRepository.UpdateMemberAsync(getTripMember);

        return new TripMemberResponseDTO
        {
            TripId = getTripMember.TripId,
            UserId = getTripMember.UserId,
            MemberStatus = getTripMember.MemberStatus,
            JoinedAt = getTripMember.JoinedAt
        };
    }

    public async Task<bool> RemoveMemberAsync(long userId, long tripId)
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
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<TripMemberResponseDTO>> GetTripMembersAsync(long tripId)
    {
        var getTrip = await _tripRepository.GetTripByIdAsync(tripId);
        if (getTrip == null)
        {
            throw new InvalidOperationException("The requested trip does not exist");
        }
        var member = await _tripMemberRepository.GetTripMembersAsync(tripId);
        var result = member.Select(m => new TripMemberResponseDTO
        {
            UserId = m.UserId,
            TripId = m.TripId,
            MemberStatus = m.MemberStatus,
            JoinedAt = m.JoinedAt
        });
        return result;
    }

    public async Task<TripMemberResponseDTO> GetTripMemberAsync(long tripId, long userId)
    {
        var getTrip = await _tripRepository.GetTripByIdAsync(tripId);
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getTrip == null)
        {
            throw new InvalidOperationException("The trip is not found");
        }
        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }

        bool isUserInTrip = await _tripMemberRepository.IsUserInTripAsync(userId, tripId);
        if (!isUserInTrip)
        {
            throw new InvalidOperationException("The user is not in this trip");
        }

        var getTripMember = await _tripMemberRepository.GetTripMemberAsync(tripId, userId);
        if (getTripMember == null)
        {
            throw new InvalidOperationException("The user is not in this trip");
        }
        return new TripMemberResponseDTO
        {
            TripId = getTripMember.TripId,
            UserId = getTripMember.UserId,
            MemberStatus = getTripMember.MemberStatus,
            JoinedAt = getTripMember.JoinedAt
        };
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

    public async Task<IEnumerable<TripResponseDTO>> GetJoinedUserUpcomingTripsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }
        var userJoinedUpcomingTrips = await _tripMemberRepository.GetJoinedUpcomingTripsByUserIdAsync(userId);
        var result = userJoinedUpcomingTrips.Select(u => new TripResponseDTO
        {
            TripId = u.TripId,
            TripOrganizerId = u.Trip.TripOrganizerId,
            Title = u.Trip.Title,
            Destination = u.Trip.Destination,
            StartDate = u.Trip.StartDate,
            EndDate = u.Trip.EndDate,
            AveragePricePerPerson = u.Trip.AveragePricePerPerson,
            Description = u.Trip.Description,
            TripImagesUrl = u.Trip.TripImagesUrl,
            IsLookingForBuddies = u.Trip.IsLookingForBuddies
        });
        return result;
    }

    public async Task<IEnumerable<TripResponseDTO>> GetJoinedUserOngoingTripsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }
        var userJoinedOngoingTrips = await _tripMemberRepository.GetJoinedOngoingTripsByUserIdAsync(userId);
        var result = userJoinedOngoingTrips.Select(u => new TripResponseDTO
        {
            TripId = u.TripId,
            TripOrganizerId = u.Trip.TripOrganizerId,
            Title = u.Trip.Title,
            Destination = u.Trip.Destination,
            StartDate = u.Trip.StartDate,
            EndDate = u.Trip.EndDate,
            AveragePricePerPerson = u.Trip.AveragePricePerPerson,
            Description = u.Trip.Description,
            TripImagesUrl = u.Trip.TripImagesUrl,
            IsLookingForBuddies = u.Trip.IsLookingForBuddies
        });
        return result;
    }

    public async Task<IEnumerable<TripResponseDTO>> GetJoinedUserPastTripsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The request user does not exist");
        }
        var userJoinedPastTrips = await _tripMemberRepository.GetJoinedPastTripsByUserIdAsync(userId);
        var result = userJoinedPastTrips.Select(u => new TripResponseDTO
        {
            TripId = u.TripId,
            TripOrganizerId = u.Trip.TripOrganizerId,
            Title = u.Trip.Title,
            Destination = u.Trip.Destination,
            StartDate = u.Trip.StartDate,
            EndDate = u.Trip.EndDate,
            AveragePricePerPerson = u.Trip.AveragePricePerPerson,
            Description = u.Trip.Description,
            TripImagesUrl = u.Trip.TripImagesUrl,
            IsLookingForBuddies = u.Trip.IsLookingForBuddies
        });
        return result;
    }

    public async Task<bool> IsUserInTripAsync(long userId, long tripId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        var getTrip = await _tripRepository.GetTripByIdAsync(tripId);

        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }

        if (getTrip == null)
        {
            throw new InvalidOperationException("The trip is not found");
        }

        return await _tripMemberRepository.IsUserInTripAsync(userId, tripId);
    }
}