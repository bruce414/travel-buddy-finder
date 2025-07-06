namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public class Hobby
{
    public long HobbyId { get; set; }

    public string Description { get; set; } = null!;

    /*Despite being a many to many relationship with User, the reason that Hobby model doesn't require a FK is because FK
    exists inside the implicit joined table between User and Hobby, which is automatically handled by EF core*/

    //Navigation property - which helps form the many to many relationship with User
    public ICollection<User> RelatedUsers { get; set; } = new List<User>();
}