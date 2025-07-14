using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface IHobbyRepository
{
    Task<IEnumerable<Hobby>> GetAllHobbiesAsync();
    Task<Hobby> GetHobbyByIdAsync(long hobbyId);
    Task AddHobbyAsync(Hobby hobby);
    Task UpdateHobbyAsync(Hobby hobby);
    Task RemoveHobbyAsync(Hobby hobby);
    Task<Hobby> GetUserHobbiesAsync(long userId);
}