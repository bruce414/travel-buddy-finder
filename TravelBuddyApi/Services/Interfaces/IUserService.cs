using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(long userId);
    Task AddUserAsync(UserCreateDTO userCreateDTO);
    Task UpdateUserAsync(User user, UserUpdateDTO userUpdateDTO);
    Task RemoveUserAsync(long userId);
}