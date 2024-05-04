using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Services;
using Microsoft.IdentityModel.Tokens;

namespace BaggageTrackerApi.Middlewares;

public class JwtMiddleware(RequestDelegate next, AppSettings appSettings)
{
    public async Task Invoke(HttpContext context, UserService userService)
    {
        var rawToken = context.Request.Headers.Authorization.FirstOrDefault();
        
        if (rawToken != null)
        {
            var token = ParseToken(rawToken);
            AddUserToContext(context, userService, token);
        }

        await next(context);
    }

    private void AddUserToContext(HttpContext context, UserService userService, string token)
    {
        var userId = ValidateToken(token);

        context.Items["User"] = userService.GetUserById(userId);
    }

    private int ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.ASCII.GetBytes(appSettings.SecretKey);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        return userId;
    }

    private static string ParseToken(string authorizationHeader) => authorizationHeader.Split(" ").Last();
}