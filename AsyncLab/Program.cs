using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");

            Stopwatch sw = Stopwatch.StartNew();

            var tasks = new List<Task>();

            for (int i = 0; i < 200; i++) {
                int id = i;
                tasks.Add(Task.Run(() => Work(id)));
            }

            await Task.WhenAll(tasks);

            sw.Stop();

            Console.WriteLine($"Done in {sw.ElapsedMilliseconds} ms");
        }

        public static void Work(int id)
        {
            // имитация CPU + blocking
            Thread.Sleep(200);

            Console.WriteLine($"Work {id} done on thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}