using BlogPlatform.Service.Common.Entities;

namespace Identity.API.Models;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public string? RefreshToken { get; set; }
}