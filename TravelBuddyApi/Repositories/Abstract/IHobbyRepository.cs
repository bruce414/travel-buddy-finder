using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface IHobbyRepository
{
    Task<IEnumerable<Hobby>> GetAllHobbiesAsync();
    Task<Hobby?> GetHobbyByIdAsync(long hobbyId);
    Task<IEnumerable<Hobby>> GetUserHobbiesAsync(long userId);
}