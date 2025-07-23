namespace TravelBuddyApi.DTOs;

public class PasswordUpdateDTO
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}