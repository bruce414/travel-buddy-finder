using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
    Task<IEnumerable<UserResponseDTO>> GetUsersWithSpecificHobbies(IEnumerable<long> hobbyIds);
    // Task<IEnumerable<UserResponseDTO>> GetUsersWithSpecificTrips(IEnumerable<long> tripIds);
    Task<UserResponseDTO> GetUserByIdAsync(long userId);
    Task<UserResponseDTO> AddUserAsync(UserCreateDTO userCreateDTO);
    Task<UserResponseDTO> UpdateUserAsync(long userId, UserUpdateDTO userUpdateDTO);
    Task<bool> RemoveUserAsync(long userId);
}