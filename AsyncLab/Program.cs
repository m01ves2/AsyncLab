using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static int counter = 0;
        public static object _lock = new object();
        public static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 50; i++) {
                ThreadPool.QueueUserWorkItem(Work);
            }


            // важно: даём ThreadPool время завершить работу
            Thread.Sleep(2000);


            sw.Stop();
            Console.WriteLine(counter);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
        }

        public static void Work(object? state)
        {
            Console.WriteLine(
                $"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}