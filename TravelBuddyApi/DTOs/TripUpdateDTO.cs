namespace TravelBuddyApi.DTOs;

using System.ComponentModel.DataAnnotations;
using TravelBuddyApi.Models;

public class TripUpdateDTO
{
    [Required(ErrorMessage = "Title cannot be null")]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Destination cannot be null")]
    [MaxLength(100)]
    public string Destination { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start date can't be null")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date can't be null")]
    public DateTime EndDate { get; set; }

    public float? AveragePricePerPerson { get; set; }

    [Required(ErrorMessage = "Please provide a brief description about the trip")]
    [MaxLength(800)]
    public string Description { get; set; } = string.Empty;

    public string? TripImagesUrl { get; set; }

    public bool IsLookingForBuddies { get; set; } = true;
}