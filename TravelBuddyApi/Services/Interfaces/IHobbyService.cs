using TravelBuddyApi.DTOs;

namespace TravelBuddyApi.Services.Interfaces;

public interface IHobbyService
{
    Task<IEnumerable<HobbyResponseDTO>> GetAllHobbiesAsync();
    Task<IEnumerable<HobbyResponseDTO>> GetUserHobbiesAsync(long userId);
    Task AddHobbyToUserAsync();
    Task RemoveHobbyFromUserAsync(long userId, long hobbyId);
}