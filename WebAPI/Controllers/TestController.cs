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

        [HttpGet("starve")]
        public IActionResult Starve()
        {
            Console.WriteLine($"START {DateTime.Now:HH:mm:ss.fff} | Thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(30000);

            Console.WriteLine($"END {DateTime.Now:HH:mm:ss.fff} | Thread {Thread.CurrentThread.ManagedThreadId}");

            return Ok();
        }
    }

}
