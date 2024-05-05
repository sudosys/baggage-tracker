using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaggageTrackerApi.Entities.DTOs;
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

    private async Task<string> GenerateJwtToken(UserSlimDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() => tokenHandler.CreateToken(PrepareTokenClaims(user)));

        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor PrepareTokenClaims(UserSlimDto user)
    {
        var key = Encoding.ASCII.GetBytes(appSettings.Value.SecretKey);
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }
}