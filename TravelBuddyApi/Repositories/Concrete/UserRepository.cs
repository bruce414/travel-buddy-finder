namespace TravelBuddyApi.Repositories.Concrete;

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
    public async Task<User?> GetUserByIdAsync(long id)
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
        users.Sort((a, b) => string.Concat(a.FirstName, " ", a.LastName)
              .CompareTo(string.Concat(b.FirstName, " ", b.LastName)));
        return users;
    }

    public async Task<IEnumerable<User>> GetUsersbyHobbyIdsAsync(IEnumerable<long> hobbyIds)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.Hobbies.Any(h => hobbyIds.Contains(h.HobbyId)))
                .Include(h => h.Hobbies)
                .ToListAsync();
    }

    /*The following three mathods serve MatchMakingService.cs*/
    public async Task<User?> GetUserWithHobbies(long userId)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.Hobbies)
                .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<long>> GetUserHobbyIds(long userId)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Hobbies.Select(u => u.HobbyId))
                .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllOtherUsersWithAtLeastOneSameHobby(long userId)
    {
        return await _travelBuddyContext.Users
                .Where(otu => otu.UserId != userId && otu.Hobbies
                .Any(h => _travelBuddyContext.Users.Where(u => u.UserId == userId).SelectMany(u => u.Hobbies.Select(u => u.HobbyId)).Contains(h.HobbyId))).Take(100) //To limit the number of users being extracted from the db
                .Include(otu => otu.Hobbies)
                .ToListAsync();
    }
}