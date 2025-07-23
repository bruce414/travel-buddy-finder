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
    Task ChangePasswordAsync(long userId, PasswordUpdateDTO passwordUpdateDTO);
    Task<bool> RemoveUserAsync(long userId);
    Task<bool> AddHobbyToUserAsync(long userId, long hobbyId);
    Task<bool> RemoveHobbyFromUserAsync(long userId, long hobbyId);
    Task<HobbyResponseDTO> GetUserHobbyAsync(long userId, long hobbyId);
}