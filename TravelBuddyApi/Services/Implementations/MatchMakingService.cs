namespace TravelBuddyApi.Services.Implementations;

using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;

//Can be simplified
public class MatchMakingRepository(TravelBuddyContext _travelBuddyContext, UserRepository _userRepository, TripMemberRepository _tripMemberRepository)
{
    public async Task<IEnumerable<MatchMaking>> GetUsersByRecommendationAsync(long userId)
    {
        //Default filtering factor is hobbies
        User? currentUser = await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.Hobbies)
                .FirstOrDefaultAsync();

        if (currentUser == null)
        {
            return [];
        }

        var currentUserHobbyIds = await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Hobbies.Select(h => h.HobbyId))
                .ToListAsync();

        var retrieveCurrentUserAllTrips = await _tripMemberRepository.GetTripsByUserIdAsync(userId);

        IEnumerable<User?> otherUsers = await _travelBuddyContext.Users
                .Where(otu => otu.UserId != userId && otu.Hobbies.Any(h => currentUserHobbyIds.Contains(h.HobbyId))).Take(100) //To limit the number of users being extracted from the db
                .Include(otu => otu.Hobbies)
                .ToListAsync();

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