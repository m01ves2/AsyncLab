using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Start");

            int result = CalculateAsync().Result;

            Console.WriteLine(result);
        }

        public static async Task<int> CalculateAsync()
        {
            await Task.Delay(2000);

            return 42;
        }
    }
}