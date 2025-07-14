using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Concrete;

public class HobbyRepository(TravelBuddyContext _travelBuddyContext)
{
    public async Task<IEnumerable<Hobby>> GetAllHobbiesAsync()
    {
        return await _travelBuddyContext.Hobbies.ToListAsync();
    }

    public async Task<Hobby?> GetHobbyByIdAsync(long hobbyId)
    {
        return await _travelBuddyContext.Hobbies
                .Where(h => h.HobbyId == hobbyId)
                .FirstOrDefaultAsync();
    }

    public async Task AddHobbyAsync(Hobby hobby)
    {
        _travelBuddyContext.Hobbies.Add(hobby);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task UpdateHobbyAsync(Hobby hobby)
    {
        _travelBuddyContext.Hobbies.Update(hobby);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task RemoveHobbyAsync(Hobby hobby)
    {
        _travelBuddyContext.Hobbies.Remove(hobby);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Hobby>> GetUserHobbiesAsync(long userId)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Hobbies)
                .ToListAsync();
    }
}