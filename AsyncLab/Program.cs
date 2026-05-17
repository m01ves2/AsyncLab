using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine($"Main start: {Thread.CurrentThread.ManagedThreadId}");

            await DemoAsync();

            Console.WriteLine($"Main resumed: {Thread.CurrentThread.ManagedThreadId}");
        }

        public static async Task DemoAsync()
        {
            Console.WriteLine($"Before await: {Thread.CurrentThread.ManagedThreadId}");

            await Task.Delay(2000);

            Console.WriteLine($"After await: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}