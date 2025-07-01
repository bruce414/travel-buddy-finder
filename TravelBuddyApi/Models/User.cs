namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    required public string Name { get; set; }

    required public string UserName { get; set; }

    required public string EmailAddress { get; set; }

    required public string PasswordHash { get; set; }

    required public string ProfileInfo { get; set; }

    //created at

}

