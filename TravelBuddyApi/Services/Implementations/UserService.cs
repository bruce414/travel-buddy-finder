using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;
using TravelBuddyApi.Services.Interfaces;

namespace TravelBuddyApi.Services.Implementations;

public class UserService(IUserRepository _userRepository)
{
    public async Task<IEnumerable<UserResponseDTO>> GetAllUsers()
    {
        var getUsers = await _userRepository.GetAllUsersAsync();

        var result = getUsers.Select(au => new UserResponseDTO
        {
            UserId = au.UserId,
            FirstName = au.FirstName,
            LastName = au.LastName,
            UserName = au.UserName,
            DateOfBirth = au.DateOfBirth,
            Gender = au.Gender,
            Nationality = au.Nationality,
            EmailAddress = au.EmailAddress,
            PasswordHash = au.PasswordHash,
            ProfileInfo = au.ProfileInfo,
            ProfileImageUrl = au.ProfileImageUrl
        });
        return result;
    }

    public async Task<User> GetUserByIdAsync(long userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task AddUserAsync(UserCreateDTO userCreateDTO)
    {
        var newUser = new User
        {
            UserId = userCreateDTO.UserId,
            FirstName = userCreateDTO.FirstName,
            LastName = userCreateDTO.LastName,
            UserName = userCreateDTO.UserName,
            DateOfBirth = userCreateDTO.DateOfBirth,
            Gender = userCreateDTO.Gender,
            Nationality = userCreateDTO.Nationality,
            EmailAddress = userCreateDTO.EmailAddress,
            PasswordHash = userCreateDTO.PasswordHash,
            ProfileInfo = userCreateDTO.ProfileInfo,
            ProfileImageUrl = userCreateDTO.ProfileImageUrl
        };

        await _userRepository.AddUserAsync(newUser);
    }

    public async Task UpdateUserAsync(long userId, UserUpdateDTO userUpdateDTO)
    {
        User getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        getUser.FirstName = userUpdateDTO.FirstName;
        getUser.LastName = userUpdateDTO.LastName;
        getUser.UserName = userUpdateDTO.UserName;
        getUser.DateOfBirth = userUpdateDTO.DateOfBirth;
        getUser.Gender = userUpdateDTO.Gender;
        getUser.Nationality = userUpdateDTO.Nationality;
        getUser.EmailAddress = userUpdateDTO.EmailAddress;
        getUser.PasswordHash = userUpdateDTO.PasswordHash;
        getUser.ProfileInfo = userUpdateDTO.ProfileImageUrl;
        getUser.ProfileImageUrl = userUpdateDTO.ProfileImageUrl;

        await _userRepository.UpdateUserAsync(getUser);
    }

    public async Task RemoveUserAsync(long userId)
    {
        User getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }
        await _userRepository.RemoveUserAsync(userId);
    }
}