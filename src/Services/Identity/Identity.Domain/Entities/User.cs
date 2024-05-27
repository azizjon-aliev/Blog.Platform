using BlogPlatform.Service.Common;

namespace Identity.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}