namespace TravelBuddyApi.Models;

using System.ComponentModel.DataAnnotations;
using System.Collections;

public class Message
{
    public int MessageId { get; set; }

    [Required]
    [MaxLength(800)]
    public string MessageContent { get; set; } = null!;

    /*FK -> Navigate to the Message owner*/
    public int SenderId { get; set; }
    public User Sender { get; set; } = null!;

    public int ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}