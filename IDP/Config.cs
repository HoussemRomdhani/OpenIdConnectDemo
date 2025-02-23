using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "Your role(s)", ["role"])
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
           new ApiScope("bookstoreapi.fullaccess", "Full access to Book Store API"),
           new ApiScope("bookstoreapi.read", "Read only access to Book Store API"),
           new ApiScope("bookstoreapi.write", "Write access to Book Store API")
        ];

    public static IEnumerable<ApiResource> ApiResources =>
        [
          new ApiResource
          {
            Name = "bookstoreapi",
            DisplayName = "Book Store API",
            Scopes = { "bookstoreapi.read", "bookstoreapi.write" }
          }
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientName = "Book Store",
                ClientId = "bookstore",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RequireConsent = true,
                RequirePkce = true,
                RedirectUris = {"https://localhost:5002/signin-oidc" },
                AllowedScopes = 
                {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "roles",
                  "bookstoreapi.read", 
                  "bookstoreapi.write"
                },
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
            },
             new Client
            {
                ClientId = "publicclient",
                ClientName = "Public Client",
                AllowedGrantTypes = GrantTypes.Code, 
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris = { "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                     "roles",
                     "bookstoreapi.read"
                },
                RequireConsent = true,
                AllowAccessTokensViaBrowser = true
            }
        ];
}