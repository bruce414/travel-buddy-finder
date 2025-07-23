using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;

namespace TravelBuddyApi.Repositories.Concrete;

public class HobbyRepository : IHobbyRepository
{
    private readonly TravelBuddyContext _travelBuddyContext;

    public HobbyRepository(TravelBuddyContext travelBuddyContext)
    {
        _travelBuddyContext = travelBuddyContext;
    }

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

    public async Task<IEnumerable<Hobby>> GetUserHobbiesAsync(long userId)
    {
        return await _travelBuddyContext.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Hobbies)
                .ToListAsync();
    }
}