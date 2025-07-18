using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Abstract;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAllMessagesAsync();
    Task<Message?> GetMessageByIdAsync(long messageId);
    Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long userId1, long userId2);
    Task<IEnumerable<User>> GetRecentContactsAsync(long userId);
    Task<long> GetUnreadMessageCountAsync(long userId);
    Task<IEnumerable<Message>> GetUnreadMessagesAsync(long userId);
    Task AddMessageAsync(Message message);
    Task UpdateMessageAsync(Message message);
    Task RemoveMessageAsync(long messageId);
    Task<bool> MessageExistsAsync(long messageId);
}