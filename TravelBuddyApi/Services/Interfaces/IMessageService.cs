using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Interfaces;

public interface IMessageService
{
    Task<MessageResponseDTO> GetMessageByIdAsync(long messageId);
    Task<bool> MessageExistsAsync(long messageId);
    // Task SendMessagesAsync(long senderId, long receiverId, string messageContent);
    Task<IEnumerable<MessageResponseDTO>> GetMessagesBetweenUsersAsync(long senderId, long receiverId);
    Task<long?> GetUnreadMessageCounts(long userId);
    Task<MessageResponseDTO> MarkMessageAsReadAsync(long messageId);
    Task<MessageResponseDTO> SendMessageAsync(MessageCreateDTO messageCreateDTO);
    Task<MessageResponseDTO> UpdateMessageAsync(MessageUpdateDTO messageUpdateDTO);
    Task<bool> RemoveMessageAsync(long messageId);
    Task<IEnumerable<User>> GetRecentContactsAsync(long userId);
}