namespace TravelBuddyApi.Repositories.Abstract;

using TravelBuddyApi.Models;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(long id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task RemoveUserAsync(long id);
    Task SaveChangesAsync();
    Task<bool> UserExistsAsync(long id);
    Task<List<User>> SortUsersByNameAsync();
}