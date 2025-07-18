namespace TravelBuddyApi.Repositories.Abstract;

using TravelBuddyApi.Models;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(long id);
    Task<IEnumerable<User>> GetUsersByHobbyIdsAsync(IEnumerable<long> hobbyIds);
    //Retrieve users with similar interests
    Task<User?> GetUsersWithHobbiesAsync(long userId);
    Task<IEnumerable<long>> GetUserHobbyIdsAsync(long userId);
    Task<IEnumerable<User>> GetAllOtherUsersWithAtLeastOneSameHobbyAsync(long userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task RemoveUserAsync(long id);
    Task<bool> UserExistsAsync(long id);
    Task<IEnumerable<User>> SortUsersByNameAsync();
}