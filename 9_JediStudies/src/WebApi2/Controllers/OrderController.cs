using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi2.Controllers
{
    [Route("api/order")]
    [Authorize]
    public class OrderController : Controller
    {
        private ILogger<OrderController> _logger;

        public OrderController(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<OrderController>();
        }

        public IActionResult Get()
        {
            _logger.LogInformation("Accessing GET: api/order action");
            return Ok(new Claim("Jedi", "Ronaldo"));
        }


    }
}
