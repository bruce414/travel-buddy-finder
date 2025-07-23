namespace TravelBuddyApi.Contexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Data;
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
            .HasForeignKey(tm => tm.TripId)
            .OnDelete(DeleteBehavior.Restrict);
        //Map the many to one relationship from TripMember to User
        modelBuilder.Entity<TripMember>()
            .HasOne(tm => tm.User)
            .WithMany(t => t.TripCollections)
            .HasForeignKey(tm => tm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

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

        modelBuilder.Entity<Hobby>().HasData(HobbyData.HobbyArray());
    }

}