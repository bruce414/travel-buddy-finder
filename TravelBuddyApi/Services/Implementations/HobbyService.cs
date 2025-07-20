using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;
using TravelBuddyApi.Repositories.Concrete;
using TravelBuddyApi.Services.Interfaces;

namespace TravelBuddyApi.Services.Implementations;

public class HobbyService : IHobbyService
{
    private readonly IHobbyRepository _hobbyRepository;
    private readonly IUserRepository _userRepository;

    public HobbyService(IHobbyRepository hobbyRepository, IUserRepository userRepository)
    {
        _hobbyRepository = hobbyRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<HobbyResponseDTO>> GetAllHobbiesAsync()
    {
        var allHobbies = await _hobbyRepository.GetAllHobbiesAsync();
        var result = allHobbies.Select(h => new HobbyResponseDTO
        {
            HobbyId = h.HobbyId,
            Description = h.Description
        });
        return result;
    }

    public async Task<IEnumerable<HobbyResponseDTO>> GetUserHobbiesAsync(long userId)
    {
        var getUser = _userRepository.GetUserByIdAsync(userId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The user does not exist");
        }

        var getHobbies = await _hobbyRepository.GetUserHobbiesAsync(userId);
        var result = getHobbies.Select(h => new HobbyResponseDTO
        {
            HobbyId = h.HobbyId,
            Description = h.Description,
        });
        return result;
    }

    public async Task<HobbyResponseDTO> GetUserHobbyAsync(long userId, long hobbyId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        var getHobby = await _hobbyRepository.GetHobbyByIdAsync(hobbyId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }
        if (getHobby == null)
        {
            throw new InvalidOperationException("The hobby is not found");
        }

        await _hobbyRepository.GetUserHobbyAsync(userId, hobbyId);

        return new HobbyResponseDTO
        {
            HobbyId = getHobby.HobbyId,
            Description = getHobby.Description
        };
    }

    public async Task<HobbyResponseDTO> AddHobbyToUserAsync(long userId, HobbyCreateDTO hobbyCreateDTO)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        var getHobby = await _hobbyRepository.GetHobbyByIdAsync(hobbyCreateDTO.HobbyId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }
        if (getHobby == null)
        {
            throw new InvalidOperationException("The hobby is not found");
        }

        Hobby addHobby = new Hobby
        {
            HobbyId = hobbyCreateDTO.HobbyId,
            Description = hobbyCreateDTO.Description
        };
        await _hobbyRepository.AddHobbyAsync(addHobby);

        return new HobbyResponseDTO
        {
            HobbyId = addHobby.HobbyId,
            Description = addHobby.Description
        };
    }

    public async Task<bool> RemoveHobbyFromUserAsync(long userId, long hobbyId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        var getHobby = await _hobbyRepository.GetHobbyByIdAsync(hobbyId);
        if (getUser == null)
        {
            throw new InvalidOperationException("The user is not found");
        }
        if (getHobby == null)
        {
            throw new InvalidOperationException("The hobby is not found");
        }

        await _hobbyRepository.RemoveHobbyAsync(getHobby);
        return true;
    }
}