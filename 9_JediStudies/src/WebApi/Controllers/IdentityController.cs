using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : Controller
    {
        private ILogger<IdentityController> _logger;

        public IdentityController(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<IdentityController>();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = new HttpClient();
            var token = await HttpContext.Authentication.GetTokenAsync("access_token");
            client.SetBearerToken(token);

            var response = await client.GetAsync("http://localhost:5004/api/order");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error on Response. {await response.Content.ReadAsStringAsync()}");
                return GetJson();
            }

            // o retorno não é utilizado, apenas em caso de erro.
            // Este exemplo testa apenas a autenticação de uma api para outra

            return GetJson();
        }

        private IActionResult GetJson()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

    }
}
