using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;

namespace TravelBuddyApi.Repositories.Concrete;

public class MessageRepository : IMessageRepository
{
    private readonly TravelBuddyContext _travelBuddyContext;

    public MessageRepository(TravelBuddyContext travelBuddyContext)
    {
        _travelBuddyContext = travelBuddyContext;
    }

    public async Task<IEnumerable<Message>> GetAllMessagesAsync()
    {
        return await _travelBuddyContext.Messages.ToListAsync();
    }

    public async Task<Message?> GetMessageByIdAsync(long messageId)
    {
        return await _travelBuddyContext.Messages.FindAsync(messageId);
    }

    public async Task<bool> MessageExistsAsync(long messageId)
    {
        return await _travelBuddyContext.Messages.AnyAsync(m => m.MessageId == messageId);
    }

    public async Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long userId1, long userId2)
    {
        return await _travelBuddyContext.Messages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) || (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetRecentContactsAsync(long userId)
    {
        var recentContactIds = await _travelBuddyContext.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .OrderByDescending(m => m.SentAt)
                .Select(i => i.SenderId == userId ? i.ReceiverId : i.SenderId)
                .Distinct()
                .ToListAsync();

        return await _travelBuddyContext.Users
                .Where(u => recentContactIds.Contains(u.UserId))
                .ToListAsync();
    }

    public async Task<long> GetUnreadMessageCountAsync(long userId)
    {
        return await _travelBuddyContext.Messages
                .Where(m => m.ReceiverId == userId && m.IsRead == false)
                .CountAsync();
    }

    public async Task<IEnumerable<Message>> GetUnreadMessagesAsync(long userId)
    {
        return await _travelBuddyContext.Messages
                .Where(m => m.ReceiverId == userId && m.IsRead == false)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();
    }

    public async Task AddMessageAsync(Message message)
    {
        _travelBuddyContext.Messages.Add(message);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task UpdateMessageAsync(Message message)
    {
        _travelBuddyContext.Messages.Update(message);
        await _travelBuddyContext.SaveChangesAsync();
    }

    public async Task RemoveMessageAsync(long messageId)
    {
        var getMessage = await _travelBuddyContext.Messages.FindAsync(messageId);
        _travelBuddyContext.Messages.Remove(getMessage!);
        await _travelBuddyContext.SaveChangesAsync();
    }
}
