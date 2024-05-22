using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity;

public static class Configuration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("BlogPlatformAdminPanel", "Blog Platform Admin Panel")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("BlogPlatformAdminPanel", "Blog Platform Admin Panel", new[] { JwtClaimTypes.Name })
            {
                Scopes = { "BlogPlatformAdminPanel" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "BlogPlatformAdminPanelId",
                ClientName = "Blog Platform Admin Panel",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "BlogPlatformAdminPanel"
                },
                RedirectUris =
                {
                    "http://localhost:8000/authentication/login-callback"
                },
                AllowedCorsOrigins =
                {
                    "http://localhost:8000"
                },
                PostLogoutRedirectUris =
                {
                    "http://localhost:8000/authentication/logout-callback"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
}