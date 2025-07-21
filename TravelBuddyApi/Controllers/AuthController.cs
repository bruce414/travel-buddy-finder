namespace TravelBuddyApi.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.DTOs;
using TravelBuddyApi.Models;
using TravelBuddyApi.Services.Implementations;
using BCrypt;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TravelBuddyContext _travelBuddyContext;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(TravelBuddyContext travelBuddyContext, JwtTokenService jwTokenService)
    {
        _travelBuddyContext = travelBuddyContext;
        _jwtTokenService = jwTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
    {
        //First retrieve the user object from the DB that matches the inputted email
        var user = await _travelBuddyContext.Users
                .SingleOrDefaultAsync(u => u.EmailAddress == loginRequest.Email);

        //if user is null or the inputted password has failed the verification process by BCrypt, retur an Unauthorized error
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid credentials");
        }

        //Call the GenerateToken method from the service layer to generate a token for this user
        var token = _jwtTokenService.GenerateToken(user);
        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
    {
        //Check if the inputted email address matches any of the email address that are already in the DB, if yes, that means 
        //this email address already exists, so either the user already has an account or he/she needs to create an account
        //using a different email address
        if (await _travelBuddyContext.Users.AnyAsync(u => u.EmailAddress == registerRequest.Email))
        {
            return BadRequest("Email already exists");
        }
        
        //Establish a new User instance to put into the DB
        var user = new User
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            UserName = registerRequest.UserName,
            DateOfBirth = registerRequest.DateOfBirth,
            Gender = registerRequest.Gender,
            Nationality = registerRequest.Nationality,
            EmailAddress = registerRequest.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
        };

        //Prompt the EF to add the user object in the schema
        _travelBuddyContext.Add(user);
        //Now ef core compares the schema and the DB to see the differences, and update all the changes
        await _travelBuddyContext.SaveChangesAsync();

        return Ok("User registered successfully");
    }
    
    //Create LoginRequest object
    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    //Create RegisterRequest object which inherits the methods from the LoginRequest
    public class RegisterRequest : LoginRequest
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string UserName { get; set; } = "";
        public required DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string Nationality { get; set; } = "";
    }
}