using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(long userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task RemoveUserAsync(long userId);

}