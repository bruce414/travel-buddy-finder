namespace TravelBuddyApi.Models;

//Create a trip filter class to make fitlering trips easier
public class TripFilter
{
    public string? Destination { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsActivelylookingForBuddies { get; set; }
    public float? AveragePricePerPerson { get; set; }
}
