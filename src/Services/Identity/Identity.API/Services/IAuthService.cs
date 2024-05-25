using Identity.API.DTO;
using Identity.API.Models;

namespace Identity.API.Services;

public interface IAuthService
{
    public Task<TokenInfo> RegisterAsync(string username, string email, string password);

    public Task<TokenInfo> LoginAsync(string username, string password);

    public Task<TokenInfo> RefreshTokenAsync(string refreshToken);

    protected Task<TokenInfo> GeneratedJwt(User user);
}