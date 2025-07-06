namespace TravelBuddyApi.Repositories.Concrete;

using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;

public class UserRepository(TravelBuddyContext _travelBuddyContext)
{
    public async Task<List<User>> GetAllUsersAsync()
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
        _travelBuddyContext.Users.Add(user);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _travelBuddyContext.Entry(user).State = EntityState.Modified;
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task RemoveUserAsync(long id)
    {
        User? UserToBeRemoved = await _travelBuddyContext.Users.FindAsync(id);
        if (UserToBeRemoved != null)
        {
            _travelBuddyContext.Remove(UserToBeRemoved);
            await _travelBuddyContext.SaveChangesAsync();
        }
    }

    public async Task<bool> UserExistsAsync(long id)
    {
        return await _travelBuddyContext.Users.AnyAsync(u => u.UserId == id);
    }

    public async Task<List<User>> SortUsersByName()
    {
        List<User> users = await _travelBuddyContext.Users.ToListAsync();
        users.Sort((a, b) => a.Name.CompareTo(b.Name));
        return users;
    }
}