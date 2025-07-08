using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;
using TravelBuddyApi.Services.Interfaces;

namespace TravelBuddyApi.Services.Implementations;

public class UserService(IUserRepository _userRepository)
{
    public async Task<User> GetUserByIdAsync(long userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddUserAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task RemoveUserAsync(long userId)
    {
        await _userRepository.RemoveUserAsync(userId);
    }
}