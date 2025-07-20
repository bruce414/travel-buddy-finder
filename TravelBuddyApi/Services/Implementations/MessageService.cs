using Microsoft.VisualBasic;
using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;
using TravelBuddyApi.Repositories.Concrete;
using TravelBuddyApi.Services.Interfaces;

namespace TravelBuddyApi.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> RemoveMessageAsync(long messageId)
    {
        var getMessage = await _messageRepository.GetMessageByIdAsync(messageId);

        if (getMessage == null)
        {
            throw new InvalidOperationException("The requested message does not exist");
        }

        await _messageRepository.RemoveMessageAsync(messageId);
        return true;
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

    public async Task<long?> GetUnreadMessageCounts(long userId)
    {
        var getUser = await _userRepository.GetUserByIdAsync(userId);

        if (getUser == null)
        {
            throw new InvalidOperationException("The requested user can't be found");
        }

        var unreadCounts = await _messageRepository.GetUnreadMessageCountAsync(userId);
        return unreadCounts;
    }

    public async Task<MessageResponseDTO> MarkMessageAsReadAsync(long messageId)
    {
        var getMessage = await _messageRepository.GetMessageByIdAsync(messageId);
        if (getMessage == null)
        {
            throw new InvalidOperationException("The requested message does not exist");
        }
        getMessage.IsRead = true;

        return new MessageResponseDTO
        {
            MessageId = getMessage.MessageId,
            MessageContent = getMessage.MessageContent,
            SenderId = getMessage.SenderId,
            ReceiverId = getMessage.ReceiverId,
            SentAt = getMessage.SentAt,
            IsRead = getMessage.IsRead
        };
    }

    public async Task<IEnumerable<MessageResponseDTO>> GetMessagesBetweenUsersAsync(long senderId, long receiverId)
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

    public async Task<MessageResponseDTO> SendMessageAsync(MessageCreateDTO messageCreateDTO)
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

        return new MessageResponseDTO
        {
            MessageId = newMessage.MessageId,
            MessageContent = newMessage.MessageContent,
            SenderId = newMessage.SenderId,
            ReceiverId = newMessage.ReceiverId,
            SentAt = newMessage.SentAt,
            IsRead = newMessage.IsRead
        };
    }

    public async Task<MessageResponseDTO> UpdateMessageAsync(MessageUpdateDTO messageUpdateDTO)
    {
        var getMessage = await _messageRepository.GetMessageByIdAsync(messageUpdateDTO.MessageId);
        if (getMessage == null)
        {
            throw new InvalidOperationException("The message does not exist");
        }

        getMessage.MessageContent = messageUpdateDTO.MessageContent;

        Message updatedMessage = new Message
        {
            MessageId = getMessage.MessageId,
            MessageContent = getMessage.MessageContent,
            SenderId = getMessage.SenderId,
            ReceiverId = getMessage.ReceiverId,
            SentAt = getMessage.SentAt,
            IsRead = getMessage.IsRead
        };

        await _messageRepository.UpdateMessageAsync(updatedMessage);

        return new MessageResponseDTO
        {
            MessageId = updatedMessage.MessageId,
            MessageContent = updatedMessage.MessageContent,
            SenderId = updatedMessage.SenderId,
            ReceiverId = updatedMessage.ReceiverId,
            SentAt = getMessage.SentAt,
            IsRead = getMessage.IsRead
        };
    }

    public async Task<MessageResponseDTO> GetMessageByIdAsync(long messageId)
    {
        var getMessage = await _messageRepository.GetMessageByIdAsync(messageId);
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
        var getMessage = await _messageRepository.GetMessageByIdAsync(messageId);
        if (getMessage == null)
        {
            throw new InvalidOperationException("The message does not exist");
        }

        return await _messageRepository.MessageExistsAsync(messageId);
    }
}