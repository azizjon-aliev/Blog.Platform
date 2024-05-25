using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API;

public class AuthOptions
{
    public const string ISSUER = "RestaurantServer";
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "2EC1EE51-1100-4347-BF22-EEB6CB8B0695"; // ключ для шифрации
    public const int LIFETIME = 40; // время жизни токена - 30 минута

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}