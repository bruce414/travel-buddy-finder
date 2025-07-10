using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(long userId);
    Task AddUserAsync(long userId, UserCreateDTO userCreateDTO);
    Task UpdateUserAsync(User user, UserUpdateDTO userUpdateDTO);
    Task RemoveUserAsync(long userId);
}