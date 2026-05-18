using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");

            var tasks = new List<Task>();

            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < 100; i++) {
                tasks.Add(HandleRequest(i));
            }

            await Task.WhenAll(tasks);

            sw.Stop();

            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms");
        }

        public static async Task HandleRequest(int id)
        {
            Console.WriteLine($"Request {id} started | Thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(1000);

            Console.WriteLine($"Request {id} finished | Thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}