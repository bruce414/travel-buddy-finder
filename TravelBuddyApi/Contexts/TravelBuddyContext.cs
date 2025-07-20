namespace TravelBuddyApi.Contexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Models;

public class TravelBuddyContext : DbContext
{
    public TravelBuddyContext(DbContextOptions<TravelBuddyContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripMember> TripMembers => Set<TripMember>();
    public DbSet<Friendship> Friendships => Set<Friendship>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Hobby> Hobbies => Set<Hobby>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Composite key for TripMember
        modelBuilder.Entity<TripMember>()
            .HasKey(tm => new { tm.TripId, tm.UserId });

        /*The below two blocks of code can be interpreted as:
            Each TripMember represents one instance of a specific user on a specific trip (very important)
            Therefore, we map each TripMember instance to ONE Trip and ONE User (HasOne)
            However, each Trip can have many members (Members property), and each User can go on many trips (TripCollection property)
            This is where the navigation properties came in handy, we can reference them in WithMany
        */

        //Map the many to one relationship from TripMember to Trip
        modelBuilder.Entity<TripMember>()
            .HasOne(tm => tm.Trip)
            .WithMany(t => t.Members)
            .HasForeignKey(tm => tm.TripId);
        //Map the many to one relationship from TripMember to User
        modelBuilder.Entity<TripMember>()
            .HasOne(tm => tm.User)
            .WithMany(t => t.TripCollections)
            .HasForeignKey(tm => tm.UserId);

        /*Conclusion:
          Self-referencing many-to-many and one-to-many relationships both require mapping from both sides, but only the many-to-many one needs a composite key.
        */

        /*Self referencing relationship between User and Friendship, both sides need to be mapped*/
        //Composite key for Friendship
        modelBuilder.Entity<Friendship>()
            .HasKey(fs => new { fs.UserId, fs.FriendId });

        modelBuilder.Entity<Friendship>()
            .HasOne(fs => fs.User)
            .WithMany(f => f.SentFriendRequests)
            .HasForeignKey(fs => fs.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Friendship>()
            .HasOne(fs => fs.Friend)
            .WithMany(f => f.ReceivedFriendRequests)
            .HasForeignKey(fs => fs.FriendId)
            .OnDelete(DeleteBehavior.NoAction);

        /*The relationship between User and Message is also a self referencing relationship, because user -> message -> user
          Thus, both sides need to be mapped.
          No composite key needs to be created, because each message has its own unique id
        */
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);

        //No need to map between User and Hobby model, because when both have a navigation property pointing to each other
        //as ICollection<T>, EF core infers this as a many to many relationship

        //Seed data for in memory testing
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FirstName = "Bruce",
                LastName = "Zhang",
                UserName = "bzha414",
                DateOfBirth = new DateTime(2004, 4, 14),
                Gender = "Male",
                Nationality = "China",
                EmailAddress = "brucezhang239@gmail.com",
                PasswordHash = "hasedPassword423",
                ProfileInfo = "Love travelling, making friends",
                ProfileImageUrl = "",
                JoinedAt = new DateTime(2025, 7, 20)
            },
            new User
            {
                UserId = 2,
                FirstName = "Fiona",
                LastName = "Wang",
                UserName = "fwan622",
                DateOfBirth = new DateTime(2001, 6, 22),
                Gender = "Female",
                Nationality = "China",
                EmailAddress = "fionawang279@gmail.com",
                PasswordHash = "sefclsaPassword295",
                ProfileInfo = "Love cooking, making friends",
                ProfileImageUrl = "",
                JoinedAt = new DateTime(2025, 6, 22)
            },
            new User
            {
                UserId = 3,
                FirstName = "Freya",
                LastName = "",
                UserName = "freya",
                DateOfBirth = new DateTime(2004, 5, 31),
                Gender = "Female",
                Nationality = "China",
                EmailAddress = "freyaxxxx879@gmail.com",
                PasswordHash = "fdkdjfkdfPassword",
                ProfileInfo = "Love eating",
                ProfileImageUrl = "",
                JoinedAt = new DateTime(2025, 5, 31)
            },
            new User
            {
                UserId = 4,
                FirstName = "Rowan",
                LastName = "Liu",
                UserName = "rliu061",
                DateOfBirth = new DateTime(2004, 8, 14),
                Gender = "Male",
                Nationality = "China",
                EmailAddress = "rliudkf22@gmail.com",
                PasswordHash = "dfdjk55Password",
                ProfileInfo = "A playboy",
                ProfileImageUrl = "",
                JoinedAt = new DateTime(2025, 7, 1)
            }
        );

        modelBuilder.Entity<Trip>().HasData(
            new Trip
            {
                TripId = 1,
                Title = "Japan 7-days",
                Destination = "Japan",
                StartDate = new DateTime(2025, 8, 9),
                EndDate = new DateTime(2025, 8, 16),
                AveragePricePerPerson = 1200,
                TripStatus = TripStatus.Upcoming,
                TripOrganizerId = 1,
                Description = "A scenic trip experience in Japan",
                IsLookingForBuddies = true
            },
            new Trip
            {
                TripId = 2,
                Title = "Switzerland 7-days",
                Destination = "Switzerland",
                StartDate = new DateTime(2025, 12, 10),
                EndDate = new DateTime(2025, 12, 17),
                AveragePricePerPerson = 1800,
                TripStatus = TripStatus.Upcoming,
                TripOrganizerId = 3,
                Description = "A scenic skiing experience in the heart of alps",
                IsLookingForBuddies = true
            },
            new Trip
            {
                TripId = 3,
                Title = "East China 7-days",
                Destination = "China",
                StartDate = new DateTime(2025, 7, 15),
                EndDate = new DateTime(2025, 7, 22),
                AveragePricePerPerson = 1500,
                TripStatus = TripStatus.InProgress,
                TripOrganizerId = 2,
                Description = "What an experience",
                IsLookingForBuddies = false,
            }
        );

        modelBuilder.Entity<TripMember>().HasData(
            new TripMember
            {
                TripId = 1,
                UserId = 1,
                MemberStatus = Role.Creator,
                JoinedAt = new DateTime(2025, 7, 9),
            },
            new TripMember
            {
                TripId = 1,
                UserId = 2,
                MemberStatus = Role.Member,
                JoinedAt = new DateTime(2025, 7, 10)
            },
            new TripMember
            {
                TripId = 1,
                UserId = 3,
                MemberStatus = Role.Member,
                JoinedAt = new DateTime(2025, 7, 11)
            }
        );

        modelBuilder.Entity<Friendship>().HasData(
            new Friendship
            {
                UserId = 1,
                FriendId = 2,
                FriendshipStatus = FriendshipStatus.Accepted,
                RequestedAt = new DateTime(2025, 7, 8),
                BecameAt = new DateTime(2025, 7, 8)
            },
            new Friendship
            {
                UserId = 1,
                FriendId = 3,
                FriendshipStatus = FriendshipStatus.Accepted,
                RequestedAt = new DateTime(2025, 7, 9),
                BecameAt = new DateTime(2025, 7, 10)
            }
        );

        modelBuilder.Entity<Message>().HasData(
            new Message
            {
                MessageId = 1,
                MessageContent = "I love you",
                SenderId = 1,
                ReceiverId = 2,
                SentAt = new DateTime(2025, 7, 20),
                IsRead = true
            },
            new Message
            {
                MessageId = 2,
                MessageContent = "I love you too",
                SenderId = 2,
                ReceiverId = 1,
                SentAt = new DateTime(2025, 7, 20),
                IsRead = true,
            }
        );

        modelBuilder.Entity<Hobby>().HasData(
            new Hobby
            {
                HobbyId = 1,
                Description = "Travel"
            },
            new Hobby
            {
                HobbyId = 2,
                Description = "Cooking"
            },
            new Hobby
            {
                HobbyId = 3,
                Description = "Eating",
            },
            new Hobby
            {
                HobbyId = 4,
                Description = "Roleplaying"
            }
        );
    }

}