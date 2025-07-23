namespace TravelBuddyApi.Repositories.Concrete;

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;


public class UserRepository : IUserRepository
{
    private readonly TravelBuddyContext _travelBuddyContext;

    public UserRepository(TravelBuddyContext travelBuddyContext)
    {
        _travelBuddyContext = travelBuddyContext;
    }

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

    public async Task<IEnumerable<User>> SortUsersByNameAsync()
    {
        List<User> users = await _travelBuddyContext.Users.ToListAsync();
        users.Sort((a, b) => string.Concat(a.FirstName, " ", a.LastName)
              .CompareTo(string.Concat(b.FirstName, " ", b.LastName)));
        return users;
    }

    public async Task<IEnumerable<User>> GetUsersByHobbyIdsAsync(IEnumerable<long> hobbyIds)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.Hobbies.Any(h => hobbyIds.Contains(h.HobbyId)))
                .Include(h => h.Hobbies)
                .ToListAsync();
    }

    /*The following three mathods serve MatchMakingService.cs*/
    public async Task<User?> GetUsersWithHobbiesAsync(long userId)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.Hobbies)
                .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<long>> GetUserHobbyIdsAsync(long userId)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Hobbies.Select(u => u.HobbyId))
                .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllOtherUsersWithAtLeastOneSameHobbyAsync(long userId)
    {
        return await _travelBuddyContext.Users
                .Where(otu => otu.UserId != userId && otu.Hobbies
                .Any(h => _travelBuddyContext.Users.Where(u => u.UserId == userId).SelectMany(u => u.Hobbies.Select(u => u.HobbyId)).Contains(h.HobbyId))).Take(100) //To limit the number of users being extracted from the db
                .Include(otu => otu.Hobbies)
                .ToListAsync();
    }

    public async Task AddHobbyToUserAsync(long userId, long hobbyId)
    {
        var user = await _travelBuddyContext.Users
                .Include(u => u.Hobbies)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            throw new ArgumentException("The user is not found");
        }

        var hobby = await _travelBuddyContext.Hobbies.FindAsync(hobbyId);
        if (hobby == null)
        {
            throw new ArgumentException("The hobby is not found");
        }

        if (!user.Hobbies.Any(h => h.HobbyId == hobbyId))
        {
            user.Hobbies.Add(hobby);
            await _travelBuddyContext.SaveChangesAsync();
        }
    }

    public async Task RemoveHobbyFromUserAsync(long userId, long hobbyId)
    {
        var user = await _travelBuddyContext.Users
                .Include(u => u.Hobbies)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            throw new ArgumentException("The user is not found");
        }

        var hobby = await _travelBuddyContext.Hobbies.FindAsync(hobbyId);
        if (hobby == null)
        {
            throw new ArgumentException("The hobby is not found");
        }

        if (user.Hobbies.Any(h => h.HobbyId == hobbyId))
        {
            user.Hobbies.Remove(hobby);
            await _travelBuddyContext.SaveChangesAsync();
        }
    }

    public async Task<Hobby?> GetUserHobbyAsync(long userId, long hobbyId)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Hobbies)
                .FirstOrDefaultAsync(h => h.HobbyId == hobbyId);
    }
}