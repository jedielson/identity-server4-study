using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            Console.Title = typeof(Program).AssemblyQualifiedName;

            // objeto para usar o endpoint de metadados
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // caso seja passado aqui um cliente errado, ou a chave "secret" errada, o retorno será "invalid_credentials"
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");

            // caso seja passado aqui um escopo incorreto, o erro será "invalid_scope"
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}