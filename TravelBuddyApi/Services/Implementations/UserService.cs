using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;
using TravelBuddyApi.Services.Interfaces;

namespace TravelBuddyApi.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHobbyRepository _hobbyRepository;

    public UserService(IUserRepository userRepositoy, IHobbyRepository hobbyRepository)
    {
        _userRepository = userRepositoy;
        _hobbyRepository = hobbyRepository;
    }

    public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
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

    public async Task<UserResponseDTO> GetUserByIdAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);

        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }

        UserResponseDTO user = new UserResponseDTO
        {
            UserId = getUser.UserId,
            FirstName = getUser.FirstName,
            LastName = getUser.LastName,
            UserName = getUser.UserName,
            DateOfBirth = getUser.DateOfBirth,
            Gender = getUser.Gender,
            Nationality = getUser.Nationality,
            EmailAddress = getUser.EmailAddress,
            ProfileInfo = getUser.ProfileInfo,
            ProfileImageUrl = getUser.ProfileImageUrl,
            Hobbies = getUser.Hobbies.Select(h => new HobbyResponseDTO
            {
                HobbyId = h.HobbyId,
                Description = h.Description
            })
        };
        return user;
    }

    public async Task<UserResponseDTO> AddUserAsync(UserCreateDTO userCreateDTO)
    {
        var newUser = new User
        {
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
            ProfileInfo = newUser.ProfileInfo,
            ProfileImageUrl = newUser.ProfileImageUrl
        };
    }

    public async Task<UserResponseDTO> UpdateUserAsync(long userId, UserUpdateDTO userUpdateDTO)
    {
        User? getUser = await _userRepository.GetUserByIdAsync(userId);
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
            ProfileInfo = getUser.ProfileInfo,
            ProfileImageUrl = getUser.ProfileImageUrl
        };
    }

    public async Task ChangePasswordAsync(long userId, PasswordUpdateDTO passwordUpdateDTO)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }

        getUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordUpdateDTO.NewPassword);

        await _userRepository.UpdateUserAsync(getUser);
    }

    public async Task<bool> RemoveUserAsync(long userId)
    {
        User? getUser = await _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            return false;
            throw new InvalidOperationException("The requested user does not exist");
        }
        await _userRepository.RemoveUserAsync(userId);

        return true;
    }

    public async Task<bool> AddHobbyToUserAsync(long userId, long hobbyId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        var getHobby = await _hobbyRepository.GetHobbyByIdAsync(hobbyId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }
        if (getHobby == null)
        {
            throw new InvalidOperationException("The hobby is not found");
        }

        await _userRepository.AddHobbyToUserAsync(userId, hobbyId);
        return true;
    }

    public async Task<bool> RemoveHobbyFromUserAsync(long userId, long hobbyId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        var getHobby = await _hobbyRepository.GetHobbyByIdAsync(hobbyId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }
        if (getHobby == null)
        {
            throw new InvalidOperationException("The hobby is not found");
        }

        await _userRepository.RemoveHobbyFromUserAsync(userId, hobbyId);
        return true;
    }

    public async Task<HobbyResponseDTO> GetUserHobbyAsync(long userId, long hobbyId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        var getHobby = await _hobbyRepository.GetHobbyByIdAsync(hobbyId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }
        if (getHobby == null)
        {
            throw new InvalidOperationException("The hobby is not found");
        }

        await _userRepository.GetUserHobbyAsync(userId, hobbyId);

        return new HobbyResponseDTO
        {
            HobbyId = getHobby.HobbyId,
            Description = getHobby.Description
        };
    }
}