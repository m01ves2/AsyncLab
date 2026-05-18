using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Console.WriteLine(
                $"START Thread: {Thread.CurrentThread.ManagedThreadId}");

            await Task.Delay(5000);

            Console.WriteLine(
                $"END Thread: {Thread.CurrentThread.ManagedThreadId}");

            return Ok();
        }
    }
}
