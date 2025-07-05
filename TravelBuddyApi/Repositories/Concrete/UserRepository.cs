namespace TravelBuddyApi.Repositories.Concrete;

using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;

public class UserRepository(TravelBuddyContext _travelBuddyContext)
{
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _travelBuddyContext.Users.ToListAsync();
    }

    //It is possible that the id we pass into the EF core does not exist in the DB, thus, the return value can be null
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _travelBuddyContext.Users.FindAsync(id);
    }

    public async Task AddUserAsync(User user)
    {
        await _travelBuddyContext.Users.AddAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        
    }
}