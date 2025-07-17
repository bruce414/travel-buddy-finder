namespace TravelBuddyApi.DTOs;

using TravelBuddyApi.Models;

public class MessageUpdateDTO
{
    public long MessageId { get; set; }

    public string MessageContent { get; set; } = null!;

    /*FK -> Navigate to the Message owner*/
    public long SenderId { get; set; }
    public User Sender { get; set; } = null!;
    public string SenderName { get; set; } = null!;

    public long ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;
    public string ReceiverName { get; set; } = null!;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
}