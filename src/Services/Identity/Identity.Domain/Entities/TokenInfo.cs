namespace Identity.Domain.Entities;

public abstract class TokenInfo
{
    public string AccessToken { get; set; } = string.Empty;
}