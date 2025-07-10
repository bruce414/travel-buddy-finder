namespace TravelBuddyApi.Repositories.Abstract;

using TravelBuddyApi.Models;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(long id);
    //Retrieve users with similar interests
    Task<User> GetUsersWithHobbiesAsync(long userId);
    Task<IEnumerable<long>> GetUserHobbyIdsAsync(long userId);
    Task<IEnumerable<User>> GetAllOtherUsersWithAtLeastOneSameHobby(long userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task RemoveUserAsync(long id);
    Task SaveChangesAsync();
    Task<bool> UserExistsAsync(long id);
    Task<List<User>> SortUsersByNameAsync();
}