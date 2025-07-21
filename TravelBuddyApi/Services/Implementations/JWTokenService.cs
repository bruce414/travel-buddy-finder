using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TravelBuddyApi.Models;

namespace TravelBuddyApi.Services.Implementations;

public class JwtTokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        //Read everything that is in JwtSettings in appsettings.json and store read values in jwtConfig
        var jwtConfig = _config.GetSection("JwtSettings");

        //Create a security secret key, the security of jwt token depends on the "Secret" value of JwtSettings.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Secret"]!));

        //Decide which security algorithem that we want to use, in this case, we pick HmacSha256
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Claims carry user's personal info, the frontend decodes these, and will know the identity of this user.
        var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress ?? ""),
            new Claim("firstName", user.FirstName ?? "")
        };

        //Establish a new jwt token for the user
        var token = new JwtSecurityToken(
            issuer: jwtConfig["Issuer"],
            audience: jwtConfig["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtConfig["ExpiryMinutes"]!)),
            signingCredentials: creds
        );

        //return the encoded token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}