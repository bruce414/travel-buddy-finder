namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public class Hobby
{
    public int HobbyId { get; set; }

    public string HobbyContents { get; set; } = null!;

    public ICollection<User> RelatedUsers { get; set; } = new List<User>();
}