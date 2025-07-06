namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public long UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public required string UserName { get; set; }

    [Required]
    public required DateTime DateOfBirth { get; set; }

    [Required]
    public required string Gender { get; set; }

    [Required]
    public required string Nationality { get; set; }

    [Required]
    public required string EmailAddress { get; set; }

    [Required]
    [MaxLength(512)]
    public required string PasswordHash { get; set; }

    [MaxLength(500)]
    public string ProfileInfo { get; set; } = "";

    public string? ProfileImageUrl { get; set; }

    //Specify the time that the user has created his profile at
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //Specify the time that the user has updated his profile at
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /*Navigation properties*/

    //Helps to form a many to many relationship with Hobby
    public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();

    public ICollection<TripMember> TripCollections { get; set; } = new List<TripMember>();

    [NotMapped]
    public IEnumerable<TripMember> UpcomingTrips => TripCollections.Where(tm => tm.TripStatus == TripStatus.Upcoming);

    [NotMapped]
    public IEnumerable<TripMember> CurrentTrips => TripCollections.Where(tm => tm.TripStatus == TripStatus.InProgress);

    [NotMapped]
    public IEnumerable<TripMember> PastTrips => TripCollections.Where(tm => tm.TripStatus == TripStatus.Past);

    public ICollection<Friendship> SentFriendRequests { get; set; } = new List<Friendship>();

    public ICollection<Friendship> ReceivedFriendRequests { get; set; } = new List<Friendship>();

    public ICollection<Message> SentMessages { get; set; } = new List<Message>();

    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}

