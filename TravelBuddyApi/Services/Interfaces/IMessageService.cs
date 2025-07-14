using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface IMessageService
{
    Task<MessageResponseDTO> GetMessageByIdAsync(long messageId);
    Task<bool> MessageExistsAsync(long messageId);
    Task SendMessagesAsync(long senderId, long receiverId, string messageContent);
    Task<IEnumerable<MessageResponseDTO>> GetMessageBetweenUser(long senderId, long receiverId);
    Task<long> GetUnreadMessageCounts(long userId);
    Task MarkMessageAsReadAsync(long messageId);
    Task AddMessageAsync(long senderId);
    Task RemoveMessageAsync(long messageId);
    Task<IEnumerable<User>> GetRecentContactsAsync(long userId);
}