using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace JediIdentityServer
{
    public class Config
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // não possui usuário interativo. Usa o clientid/secret para autenticação
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret para autenticação
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = {"api1"}
                },
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =  {"api1"}
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "Mvc Client",
                    // Invés de Implicit, use Hybrid and ClientCredentials para permitir que o Cliente MVC acesse a api de recursos
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    // Passa a ser necessário, para que o cliente consiga o access token atráves do canal em backgorund
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())  
                    },

                    RedirectUris =
                    {
                        "http://localhost:5002/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:5002/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1" // aqui é setado que o cliente pode acessar o escopo (API) de recursos
                    },

                    // Necessário habilitar o acesso offline, pois isso permite a requisição de tokens para o acesso prolongado à API
                    AllowOfflineAccess = true
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }

    }
}
