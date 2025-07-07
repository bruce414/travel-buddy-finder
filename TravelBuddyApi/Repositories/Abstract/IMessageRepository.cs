using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface IMessageRepository
{
    Task<IList<Message>> GetAllMessagesAsync();
    Task<Message> GetMessageByIdAsync(long messageId);
}