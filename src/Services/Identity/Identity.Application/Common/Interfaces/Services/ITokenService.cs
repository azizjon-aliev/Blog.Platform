using Identity.Domain.Entities;

namespace Identity.Application.Common.Interfaces.Services;

public interface ITokenService
{
    public TokenInfo GenerateToken(User user);
}