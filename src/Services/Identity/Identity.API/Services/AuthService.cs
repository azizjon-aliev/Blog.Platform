using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Identity.API.DataProvider;
using Identity.API.DTO;
using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Services;

public class AuthService(IdentityDbContext context) : IAuthService
{
    public async Task<TokenInfo> RegisterAsync(string username, string email, string password)
    {
        if (await context.Users.AnyAsync(x => x.Username == username))
            throw new ArgumentException("Username is already taken.");

        if (await context.Users.AnyAsync(x => x.Email == email))
            throw new ArgumentException("Email is already taken.");

        var user = new User
        {
            Username = username,
            Email = email,
            Password = PasswordService.HashPassword(password)
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return await GeneratedJwt(user);
    }

    public async Task<TokenInfo> LoginAsync(string username, string password)
    {
        var user = await context.Users.SingleOrDefaultAsync(x =>
            x.Username == username && x.Password == PasswordService.HashPassword(password));

        if (user is null)
            throw new ArgumentException("Invalid username or password.");

        return await GeneratedJwt(user);
    }

    public async Task<TokenInfo> RefreshTokenAsync(string refreshToken)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);

        if (user is null)
            throw new ArgumentException("Invalid refresh token.");

        return await GeneratedJwt(user);
    }

    public async Task<TokenInfo> GeneratedJwt(User user)
    {
        if (user is null)
            throw new ArgumentException("Invalid username or password.");

        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var expireTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
        var refreshToken = Guid.NewGuid().ToString();
        user.RefreshToken = refreshToken;

        context.Update(user);
        await context.SaveChangesAsync();

        return new TokenInfo { AccessToken = accessToken, RefreshToken = refreshToken, ExpireTime = expireTime };
    }
}