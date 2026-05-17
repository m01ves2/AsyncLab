using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Before await");

            await Task.Delay(2000);

            Console.WriteLine("After await");
        }
    }
}