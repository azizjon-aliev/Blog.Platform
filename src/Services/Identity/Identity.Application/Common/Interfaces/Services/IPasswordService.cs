namespace Identity.Application.Common.Interfaces.Services;

public interface IPasswordService
{
    public bool VerifyPassword(string password, string hash);

    public string HashPassword(string password);
}