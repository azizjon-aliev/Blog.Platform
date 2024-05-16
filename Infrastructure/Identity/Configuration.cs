using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity;

public static class Configuration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("BlogPlatformAPI", "Blog Platform API")
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
            new ApiResource("BlogPlatformAPI", "Blog Platform API", new[] { JwtClaimTypes.Name })
            {
                Scopes = { "BlogPlatformAPI" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "blog-platform-client",
                ClientName = "Blog Platform Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = new List<string>
                {
                    "https://.../signin-oidc"
                },
                AllowedCorsOrigins =
                {
                    "https://..."
                },
                PostLogoutRedirectUris =
                {
                    "https://.../signout-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "BlogPlatformAPI",
                },
                AllowAccessTokensViaBrowser = true
            }
        };
}