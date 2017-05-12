using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace JediIdentityServer
{
    public class Config
    {

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
                }
        }
    }
}
