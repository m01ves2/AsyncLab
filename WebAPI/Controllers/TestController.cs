using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("load-async")]
        public async Task<IActionResult> LoadAsync()
        {
            List<Task> tasks = new();

            for (int i = 0; i < 1; i++) {
                tasks.Add(Task.Delay(5000));
            }

            await Task.WhenAll(tasks);

            return Ok();
        }


        [HttpGet("load-sync")]
        public IActionResult LoadSync()
        {
            List<Task> tasks = new();

            for (int i = 0; i < 100; i++) {
                tasks.Add(Task.Run(() =>
                {
                    Thread.Sleep(5000);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            return Ok();
        }
    }
}
