namespace TravelBuddyApi.Controllers;

using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Services.Implementations;
using TravelBuddyApi.Services.Interfaces;

[ApiController]
[Route("[controller]")]
public class MessageController(IMessageService _messageService) : ControllerBase
{
    //Get message by Id
    [HttpGet("{messageId}")]
    public async Task<IActionResult> GetMessageByIdAsync(long messageId)
    {
        try
        {
            var getMessage = await _messageService.GetMessageByIdAsync(messageId);
            if (getMessage == null)
            {
                return NotFound();
            }
            return Ok(getMessage);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get the number of unread messages
    [HttpGet("{userId}/unread-count")]
    public async Task<IActionResult> GetUnreadMessagesCountsAsync(long userId)
    {
        try
        {
            var getUnreadCounts = await _messageService.GetUnreadMessageCounts(userId);
            if (getUnreadCounts == null)
            {
                return NotFound();
            }
            if (getUnreadCounts == 0)
            {
                return NotFound("There are 0 unread messages");
            }
            return Ok(new MessageCountResponseDTO { UnreadCounts = getUnreadCounts.Value });
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    //Get Messages between users
    [HttpGet("between/{senderId}/and/{receiverId}")]
    public async Task<IActionResult> GetMessagesBetweenUsersAsync(long senderId, long receiverId)
    {
        try
        {
            var getMessages = await _messageService.GetMessageBetweenUser(senderId, receiverId);
            if (getMessages == null)
            {
                return NotFound();
            }
            return Ok(getMessages);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
        }
    }

    //Get recent contacts
    [HttpGet("{userId}/recent-contacts")]
    public async Task<IActionResult> GetUserRecentContactsAsync(long userId)
    {
        try
        {
            var getUserRecentContactsAsync = await _messageService.GetRecentContactsAsync(userId);
            if (getUserRecentContactsAsync == null)
            {
                return NotFound();
            }
            return Ok(getUserRecentContactsAsync);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
        }
    }

    //Send message / Add message
    [HttpPost]
    public async Task<IActionResult> SendMessageAsync([FromBody] MessageCreateDTO messageCreateDTO)
    {
        try
        {
            var sendMessage = await _messageService.SendMessageAsync(messageCreateDTO);
            if (sendMessage == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetMessageByIdAsync), new { messageId = sendMessage.MessageId }, sendMessage);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessageAsync([FromBody] MessageUpdateDTO messageUpdateDTO)
    {
        try
        {
            var updateMessage = await _messageService.UpdateMessageAsync(messageUpdateDTO);
            if (updateMessage == null)
            {
                return NotFound();
            }
            return Ok(updateMessage);
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }

    [HttpPut("{userId}/mark-as-read")]
    public async Task<IActionResult> MarkMessageAsReadAsync(long userId)
    {
        try
        {
            var markAsRead = await _messageService.MarkMessageAsReadAsync(userId);
            if (markAsRead == null)
            {
                return NotFound();
            }
            return Ok(markAsRead);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving the messages between the users.");
        }
    }

    [HttpDelete("{messageId}")]
    public async Task<IActionResult> DeleteMessageAsync(long messageId)
    {
        try
        {
            var didRemove = await _messageService.RemoveMessageAsync(messageId);
            if (!didRemove)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
            "An error occurred while updating the user.");
        }
    }
}