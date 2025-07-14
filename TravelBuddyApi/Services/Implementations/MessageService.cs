using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Concrete;

namespace TravelBuddyApi.Services.Implementations;

public class MessageService(MessageRepository _messageRepository, UserRepository _userRepository)
{
    public async Task AddMessageAsync(long senderId, MessageCreateDTO messageCreateDTO)
    {
        var getSender = await _userRepository.GetUserByIdAsync(senderId);
        var getReceiver = await _userRepository.GetUserByIdAsync(messageCreateDTO.ReceiverId);
        if (getSender == null || getReceiver == null)
        {
            throw new InvalidOperationException("The requested user is not found");
        }

        Message newMessage = new Message
        {
            SenderId = senderId,
            Sender = getSender,
            ReceiverId = messageCreateDTO.ReceiverId,
            Receiver = getReceiver,
            SentAt = DateTime.Now,
            IsRead = false
        };

        await _messageRepository.AddMessageAsync(newMessage);
    }

    public async Task RemoveMessageAsync(long userId, long messageId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);
        var getMessage = await _messageRepository.GetMessageById(messageId);

        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user can't be found");
        }
        if (getMessage == null)
        {
            throw new InvalidOperationException("The requested message does not exist");
        }
        //Because we don't know is the userId the Sender's userId or Receiver's userId. If it's receiver's userId, receiver can't delete a message that's sent by sender.
        if (getMessage.SenderId != userId)
        {
            throw new UnauthorizedAccessException("Cannot delete someone else's message");
        }

        await _messageRepository.RemoveMessageAsync(getMessage);
    }

    public async Task<IEnumerable<User>> GetRecentContactsAsync(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);

        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user can't be found");
        }

        return await _messageRepository.GetRecentContactsAsync(userId);
    }

    public async Task<long> GetUnreadMessageCounts(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);

        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user can't be found");
        }

        return await _messageRepository.GetUnreadMessageCountAsync(userId);
    }

    public async Task MarkMessageAsRead(long messageId)
    {
        var getMessage = await _messageRepository.GetMessageById(messageId);
        if (getMessage == null)
        {
            throw new InvalidOperationException("The requested message does not exist");
        }
        getMessage.IsRead = true;
    }

    public async Task<IEnumerable<MessageResponseDTO>> GetMessagesBetweenUsers(long senderId, long receiverId)
    {
        var getSender = await _userRepository.GetUserByIdAsync(senderId);
        var getReceiver = await _userRepository.GetUserByIdAsync(receiverId);
        if (getSender == null || getReceiver == null)
        {
            throw new InvalidOperationException("The requested user is not found");
        }

        var getMessages = await _messageRepository.GetMessagesBetweenUsersAsync(senderId, receiverId);
        var result = getMessages.Select(m => new MessageResponseDTO
        {
            SenderId = m.SenderId,
            SenderName = m.Sender.FirstName + " " + m.Sender.LastName,
            ReceiverId = m.ReceiverId,
            ReceiverName = m.Receiver.FirstName + " " + m.Receiver.LastName,
            MessageContent = m.MessageContent,
            SentAt = m.SentAt,
            IsRead = m.IsRead
        });
        return result;
    }

    public async Task SendMessageAsync(MessageCreateDTO messageCreateDTO)
    {
        var getSender = await _userRepository.GetUserByIdAsync(messageCreateDTO.SenderId);
        var getReceiver = await _userRepository.GetUserByIdAsync(messageCreateDTO.ReceiverId);
        if (getSender == null)
        {
            throw new InvalidOperationException("The sender is not found");
        }
        if (getReceiver == null)
        {
            throw new InvalidOperationException("The receiver is not found");
        }

        Message newMessage = new Message
        {
            SenderId = messageCreateDTO.SenderId,
            ReceiverId = messageCreateDTO.ReceiverId,
            MessageContent = messageCreateDTO.MessageContent,
            SentAt = DateTime.Now,
            IsRead = false
        };
        await _messageRepository.AddMessageAsync(newMessage);
    }

    public async Task<MessageResponseDTO> GetMessageById(long messageId)
    {
        var getMessage = await _messageRepository.GetMessageById(messageId);
        if (getMessage == null)
        {
            throw new InvalidOperationException("The message does not exist");
        }

        var result = new MessageResponseDTO
        {
            SenderId = getMessage.SenderId,
            SenderName = getMessage.Sender.FirstName + " " + getMessage.Sender.LastName,
            ReceiverId = getMessage.ReceiverId,
            ReceiverName = getMessage.Receiver.FirstName + " " + getMessage.Receiver.LastName,
            MessageContent = getMessage.MessageContent,
            SentAt = getMessage.SentAt,
            IsRead = getMessage.IsRead
        };
        return result;
    }

    public async Task<bool> MessageExistsAsync(long messageId)
    {
        var getMessage = await _messageRepository.GetMessageById(messageId);
        if (getMessage == null)
        {
            throw new InvalidOperationException("The message does not exist");
        }

        return await _messageRepository.MessageExistsAsync(messageId);
    }
}