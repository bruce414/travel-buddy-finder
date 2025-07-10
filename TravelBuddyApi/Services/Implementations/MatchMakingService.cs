namespace TravelBuddyApi.Services.Implementations;

using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;

//Can be simplified
public class MatchMakingService(UserRepository _userRepository, TripMemberRepository _tripMemberRepository)
{
    public async Task<IEnumerable<MatchMaking>> GetUsersByRecommendationAsync(long userId)
    {
        //Default filtering factor is hobbies
        User? currentUser = await _userRepository.GetUserWithHobbies(userId);

        if (currentUser == null)
        {
            return [];
        }

        var currentUserHobbyIds = await _userRepository.GetUserHobbyIds(userId);

        var retrieveCurrentUserAllTrips = await _tripMemberRepository.GetTripsByUserIdAsync(userId);

        IEnumerable<User?> otherUsers = await _userRepository.GetAllOtherUsersWithAtLeastOneSameHobby(userId);

        if (otherUsers == Enumerable.Empty<User>())
        {
            return [];
        }

        var matchedUsers = new List<MatchMaking>();

        foreach (User? matchedUser in otherUsers)
        {
            var matchingScores = 0;

            if (matchedUser != null)
            {

                var numOfSharedHobbies = matchedUser.Hobbies
                        .Select(otu => otu.HobbyId)
                        .Intersect(currentUser.Hobbies.Select(cu => cu.HobbyId))
                        .Count();
                matchingScores += numOfSharedHobbies * 5;

                var numOfSharedTrips = retrieveCurrentUserAllTrips
                        .Select(u => u.TripId)
                        .Intersect(matchedUser.TripCollections.Select(mu => mu.TripId))
                        .Count();
                matchingScores += numOfSharedTrips * 5;

                if (matchedUser.Nationality == currentUser.Nationality)
                {
                    matchingScores += 5;
                }

                if (Math.Abs(matchedUser.DateOfBirth.Year - currentUser.DateOfBirth.Year) <= 3)
                {
                    matchingScores += 5;
                }
            }

            matchedUsers.Add(new MatchMaking { MatchedUser = matchedUser, MatchingScores = matchingScores });
        }
        return matchedUsers.OrderByDescending(mu => mu.MatchingScores);
    }
}