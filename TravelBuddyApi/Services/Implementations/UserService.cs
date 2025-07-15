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

    public async Task<IEnumerable<UserResponseDTO>> GetUsersWithSpecificHobbies(IEnumerable<long> hobbyIds)
    {
        if (!hobbyIds.Any())
        {
            return Enumerable.Empty<UserResponseDTO>();
        }

        var getUsers = await _userRepository.GetUsersByHobbyIdsAsync(hobbyIds);
        var result = getUsers.Select(u => new UserResponseDTO
        {
            UserId = u.UserId,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            DateOfBirth = u.DateOfBirth,
            Gender = u.Gender,
            Nationality = u.Nationality,
            EmailAddress = u.EmailAddress,
            PasswordHash = u.PasswordHash,
            ProfileInfo = u.ProfileInfo,
            ProfileImageUrl = u.ProfileImageUrl,
            Hobbies = u.Hobbies.Select(h => new HobbyResponseDTO
            {
                HobbyId = h.HobbyId,
                Description = h.Description
            })
        });
        return result;
    }

    public async Task<User> GetUserByIdAsync(long userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task<UserResponseDTO> AddUserAsync(UserCreateDTO userCreateDTO)
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
        return new UserResponseDTO
        {
            UserId = newUser.UserId,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            UserName = newUser.UserName,
            DateOfBirth = newUser.DateOfBirth,
            Gender = newUser.Gender,
            Nationality = newUser.Nationality,
            EmailAddress = newUser.EmailAddress,
            PasswordHash = newUser.PasswordHash,
            ProfileInfo = newUser.ProfileInfo,
            ProfileImageUrl = newUser.ProfileImageUrl
        };
    }

    public async Task<UserResponseDTO> UpdateUserAsync(long userId, UserUpdateDTO userUpdateDTO)
    {
        User getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user does not exist");
        }

        //Does not update the userId, cause if u do, what's the point!!!
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

        return new UserResponseDTO
        {
            UserId = userId,
            FirstName = getUser.FirstName,
            LastName = getUser.LastName,
            DateOfBirth = getUser.DateOfBirth,
            Gender = getUser.Gender,
            Nationality = getUser.Nationality,
            EmailAddress = getUser.EmailAddress,
            PasswordHash = getUser.PasswordHash,
            ProfileInfo = getUser.ProfileInfo,
            ProfileImageUrl = getUser.ProfileImageUrl
        };
    }

    public async Task<bool> RemoveUserAsync(long userId)
    {
        User getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            return false;
            throw new InvalidOperationException("The requested user does not exist");
        }
        await _userRepository.RemoveUserAsync(userId);

        return true;
    }
}