using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Main start thread: {Thread.CurrentThread.ManagedThreadId}");

            Task task = DoWorkAsync();

            Console.WriteLine("Task started");

            await task;

            Console.WriteLine($"Main resumed thread: {Thread.CurrentThread.ManagedThreadId}");
        }

        public static async Task DoWorkAsync()
        {
            Console.WriteLine($"Before await: {Thread.CurrentThread.ManagedThreadId}");

            await Task.Delay(2000);

            Console.WriteLine($"After await: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}