namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;

public class LocalGuide
{
    public long LocalGuideId { get; set; }

    public string LocalGuideName { get; set; } = null!;

}