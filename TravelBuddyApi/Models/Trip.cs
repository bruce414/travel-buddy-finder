namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public class Trip
{
    public int TripId { get; set; }

    required public string Title { get; set; }

    public string Destination { get; set; } = null!;

    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime EndDate { get; set; } = DateTime.UtcNow;

    public string? Description { get; set; }

    public string? TripImagesUrl { get; set; }

    //Attribute to check if the organizer is actively looking for buddies
    public bool IsLookingForBuddies { get; set; } = true;

    /*FK -> Navigate to the trip organizer*/
    public int TripOrganizerId { get; set; }
    public User TripOrganizer { get; set; } = null!;

    public ICollection<TripMember> Members { get; set; } = new List<TripMember>();
}