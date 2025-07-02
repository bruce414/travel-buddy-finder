namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Collections.Generic;

public class User
{
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    required public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    required public string UserName { get; set; }

    [Required]
    required public DateTime DateOfBirth { get; set; }

    [Required]
    required public string Gender { get; set; }

    [Required]
    required public string Nationality { get; set; }

    [Required]
    required public string EmailAddress { get; set; }

    [Required]
    required public string PasswordHash { get; set; }

    [MaxLength(500)]
    required public string ProfileInfo { get; set; }

    public string? ProfileImageUrl { get; set; }

    //Specify the time that the user has created his profile at
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //Specify the time that the user has updated his profile at
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();

    public ICollection<TripMember> UpcomingTrips { get; set; } = new List<TripMember>();

    public ICollection<TripMember> PastTrips { get; set; } = new List<TripMember>();

    public ICollection<TripMember> JoinedTrips { get; set; } = new List<TripMember>();

    public ICollection<FriendShip> SentFriendRequests { get; set; } = new List<FriendShip>();

    public ICollection<FriendShip> ReceivedFriendRequests { get; set; } = new List<FriendShip>();

    public ICollection<Message> SentMessages { get; set; } = new List<Message>();

    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}

