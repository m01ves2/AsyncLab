using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("boom")]
        public IActionResult Boom()
        {
            Console.WriteLine("CONTROLLER");

            throw new Exception("Kaboom");
        }
    }
}
