using TravelBuddyApi.DTOs;

namespace TravelBuddyApi.Services.Interfaces;

public interface IHobbyService
{
    Task<IEnumerable<HobbyResponseDTO>> GetAllHobbiesAsync();
    Task<IEnumerable<HobbyResponseDTO>> GetUserHobbiesAsync(long userId);
    Task<HobbyResponseDTO> GetUserHobbyAsync(long userId, long hobbyId);
    Task<HobbyResponseDTO> AddHobbyToUserAsync(long userId, HobbyCreateDTO hobbyCreateDTO);
    Task<bool> RemoveHobbyFromUserAsync(long userId, long hobbyId);
}