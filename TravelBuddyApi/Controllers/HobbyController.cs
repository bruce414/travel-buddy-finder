namespace TravelBuddyApi.Controllers;

using TravelBuddyApi.Services.Interfaces;
using TravelBuddyApi.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HobbyController(IHobbyService _hobbyService) : ControllerBase
{
    
}