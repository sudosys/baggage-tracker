using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Extensions;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Models.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BaggageTrackerApi.Services;

public class AuthenticationService(UserService userService, IOptions<AppSettings> appSettings)
{
    public async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest request)
    {
        var user = userService.CheckUserCredentials(request.Username, request.Password.Sha256Hash());
        
        if (user == null)
        {
            return new AuthenticationResponse(AuthenticationStatus.Failure, null, null);
        }

        var token = await GenerateJwtToken(user);

        return new AuthenticationResponse(AuthenticationStatus.Success, user, token);
    }

    private async Task<string> GenerateJwtToken(User userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() => tokenHandler.CreateToken(PrepareTokenClaims(userDto)));

        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor PrepareTokenClaims(User userDto)
    {
        var key = Encoding.ASCII.GetBytes(appSettings.Value.SecretKey);
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userDto.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }
}