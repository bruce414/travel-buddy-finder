﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelBuddyApi.Contexts;

#nullable disable

namespace TravelBuddyApi.Migrations
{
    [DbContext(typeof(TravelBuddyContext))]
    [Migration("20250722112056_SeedHobbies")]
    partial class SeedHobbies
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HobbyUser", b =>
                {
                    b.Property<long>("HobbiesHobbyId")
                        .HasColumnType("bigint");

                    b.Property<long>("RelatedUsersUserId")
                        .HasColumnType("bigint");

                    b.HasKey("HobbiesHobbyId", "RelatedUsersUserId");

                    b.HasIndex("RelatedUsersUserId");

                    b.ToTable("HobbyUser");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.Friendship", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("FriendId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("BecameAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FriendshipStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.Hobby", b =>
                {
                    b.Property<long>("HobbyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("HobbyId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HobbyId");

                    b.ToTable("Hobbies");

                    b.HasData(
                        new
                        {
                            HobbyId = 1L,
                            Description = "Acting"
                        },
                        new
                        {
                            HobbyId = 2L,
                            Description = "Art Collecting"
                        },
                        new
                        {
                            HobbyId = 3L,
                            Description = "Being a DJ"
                        },
                        new
                        {
                            HobbyId = 4L,
                            Description = "Caligraphy"
                        },
                        new
                        {
                            HobbyId = 5L,
                            Description = "Crocheting"
                        },
                        new
                        {
                            HobbyId = 6L,
                            Description = "Dancing"
                        },
                        new
                        {
                            HobbyId = 7L,
                            Description = "Designing clothing"
                        },
                        new
                        {
                            HobbyId = 8L,
                            Description = "Drawing"
                        },
                        new
                        {
                            HobbyId = 9L,
                            Description = "Freestyling"
                        },
                        new
                        {
                            HobbyId = 10L,
                            Description = "Glassblowing"
                        },
                        new
                        {
                            HobbyId = 11L,
                            Description = "Graphic design"
                        },
                        new
                        {
                            HobbyId = 12L,
                            Description = "Jewelry making"
                        },
                        new
                        {
                            HobbyId = 13L,
                            Description = "Improvisation"
                        },
                        new
                        {
                            HobbyId = 14L,
                            Description = "LARPing"
                        },
                        new
                        {
                            HobbyId = 15L,
                            Description = "Metal working"
                        },
                        new
                        {
                            HobbyId = 16L,
                            Description = "Needlepoint"
                        },
                        new
                        {
                            HobbyId = 17L,
                            Description = "Origami"
                        },
                        new
                        {
                            HobbyId = 18L,
                            Description = "Painting"
                        },
                        new
                        {
                            HobbyId = 19L,
                            Description = "Photography"
                        },
                        new
                        {
                            HobbyId = 20L,
                            Description = "Playing a musical instrument"
                        },
                        new
                        {
                            HobbyId = 21L,
                            Description = "Podcasting"
                        },
                        new
                        {
                            HobbyId = 22L,
                            Description = "Poetry"
                        },
                        new
                        {
                            HobbyId = 23L,
                            Description = "Quilting"
                        },
                        new
                        {
                            HobbyId = 24L,
                            Description = "Record collecting"
                        },
                        new
                        {
                            HobbyId = 25L,
                            Description = "Scrapbooking"
                        },
                        new
                        {
                            HobbyId = 26L,
                            Description = "Soap making"
                        },
                        new
                        {
                            HobbyId = 27L,
                            Description = "Stand-up comedy"
                        },
                        new
                        {
                            HobbyId = 28L,
                            Description = "Weaving"
                        },
                        new
                        {
                            HobbyId = 29L,
                            Description = "Web design"
                        },
                        new
                        {
                            HobbyId = 30L,
                            Description = "Welding"
                        });
                });

            modelBuilder.Entity("TravelBuddyApi.Models.Message", b =>
                {
                    b.Property<long>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MessageId"));

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.Trip", b =>
                {
                    b.Property<long>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TripId"));

                    b.Property<float?>("AveragePricePerPerson")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsLookingForBuddies")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TripImagesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TripOrganizerId")
                        .HasColumnType("bigint");

                    b.Property<int>("TripStatus")
                        .HasColumnType("int");

                    b.HasKey("TripId");

                    b.HasIndex("TripOrganizerId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.TripMember", b =>
                {
                    b.Property<long>("TripId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberStatus")
                        .HasColumnType("int");

                    b.HasKey("TripId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TripMembers");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileInfo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HobbyUser", b =>
                {
                    b.HasOne("TravelBuddyApi.Models.Hobby", null)
                        .WithMany()
                        .HasForeignKey("HobbiesHobbyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelBuddyApi.Models.User", null)
                        .WithMany()
                        .HasForeignKey("RelatedUsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelBuddyApi.Models.Friendship", b =>
                {
                    b.HasOne("TravelBuddyApi.Models.User", "Friend")
                        .WithMany("ReceivedFriendRequests")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TravelBuddyApi.Models.User", "User")
                        .WithMany("SentFriendRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Friend");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.Message", b =>
                {
                    b.HasOne("TravelBuddyApi.Models.User", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TravelBuddyApi.Models.User", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.Trip", b =>
                {
                    b.HasOne("TravelBuddyApi.Models.User", "TripOrganizer")
                        .WithMany()
                        .HasForeignKey("TripOrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TripOrganizer");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.TripMember", b =>
                {
                    b.HasOne("TravelBuddyApi.Models.Trip", "Trip")
                        .WithMany("Members")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravelBuddyApi.Models.User", "User")
                        .WithMany("TripCollections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trip");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.Trip", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("TravelBuddyApi.Models.User", b =>
                {
                    b.Navigation("ReceivedFriendRequests");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("SentFriendRequests");

                    b.Navigation("SentMessages");

                    b.Navigation("TripCollections");
                });
#pragma warning restore 612, 618
        }
    }
}
