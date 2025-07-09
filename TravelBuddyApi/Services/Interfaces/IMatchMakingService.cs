namespace TravelBuddyApi.Services.Interfaces;

using TravelBuddyApi.Models;

public interface IMatchMakingService
{
    Task<IEnumerable<MatchMaking>> GetUsersByRecommendationAsync(long userId);
}