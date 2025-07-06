namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public class Trip
{
    public long TripId { get; set; }

    public required string Title { get; set; }

    [Required]
    [MaxLength(250)]
    public string Destination { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public string? TripImagesUrl { get; set; }

    //Attribute to check if the organizer is actively looking for buddies
    public bool IsLookingForBuddies { get; set; } = true;

    /*FK -> Navigate to the trip organizer, pairs with TripOrganizer*/
    public long TripOrganizerId { get; set; }

    //Navigation properties
    public User TripOrganizer { get; set; } = null!;
    public ICollection<TripMember> Members { get; set; } = new List<TripMember>();
}