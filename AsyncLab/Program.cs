using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Before");

            await SimpleAsync();

            Console.WriteLine("After");
        }

        public static async Task SimpleAsync()
        {
            Console.WriteLine("Inside method");
        }
    }
}