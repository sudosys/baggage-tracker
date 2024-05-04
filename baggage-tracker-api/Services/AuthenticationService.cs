using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Models.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace BaggageTrackerApi.Services;

public class AuthenticationService(UserService userService, AppSettings appSettings)
{
    public async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest request)
    {
        var user = userService.CheckUserCredentials(request.Username, HashPassword(request.Password));
        
        if (user == null)
        {
            return new AuthenticationResponse(AuthenticationStatus.Failure, null, null);
        }

        var token = await GenerateJwtToken(user);

        return new AuthenticationResponse(AuthenticationStatus.Success, user, token);
    }

    private static string HashPassword(string rawPassword)
    {
        var bytes = Encoding.UTF8.GetBytes(rawPassword);
        var hashed = SHA256.HashData(bytes).ToString();

        if (hashed == null)
        {
            throw new ArgumentNullException();
        }
            
        return hashed;
    }

    private async Task<string> GenerateJwtToken(UserDto userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() => tokenHandler.CreateToken(PrepareTokenClaims(userDto)));

        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor PrepareTokenClaims(UserDto userDto)
    {
        var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userDto.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }
}