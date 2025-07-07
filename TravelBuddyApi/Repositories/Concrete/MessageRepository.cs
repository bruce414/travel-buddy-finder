using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Repositories.Concrete;

public class MessageRepository(TravelBuddyContext _travelBuddyContext)
{
    public async Task<List<Message>> GetAllMessagesAsync()
    {
        return await _travelBuddyContext.Messages.ToListAsync();
    }

    public async Task<Message?> GetMessageById(long messageId)
    {
        return await _travelBuddyContext.Messages.FindAsync(messageId);
    }

    public async Task<bool> MessageExistsAsync(long messageId)
    {
        return await _travelBuddyContext.Messages.AnyAsync(m => m.MessageId == messageId);
    }
}