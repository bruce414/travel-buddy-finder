namespace TravelBuddyApi.Models;

public class MatchMakingDto
{
    public User? MatchedUser { get; set; }
    public int MatchingScore { get; set; }
}