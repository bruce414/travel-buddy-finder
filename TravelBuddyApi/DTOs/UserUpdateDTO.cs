using System.ComponentModel.DataAnnotations;

namespace TravelBuddyApi.DTOs;

public class UserUpdateDTO
{
    [Required(ErrorMessage = "User first name can't be null")]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "User last name can't be null")]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "User username can't be null")]
    [MaxLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public string Nationality { get; set; } = string.Empty;

    [Required]
    public string EmailAddress { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(500)]
    public string ProfileInfo { get; set; } = string.Empty;

    public string ProfileImageUrl { get; set; } = string.Empty;
}