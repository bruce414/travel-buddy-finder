namespace TravelBuddyApi.Repositories.Abstract;

using TravelBuddyApi.Models;

public interface IMatchMakingRepository
{
    Task<IEnumerable<MatchMaking>> GetUsersByRecommendationAsync(long userId);
}