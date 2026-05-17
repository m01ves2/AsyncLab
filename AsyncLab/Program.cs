using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Before");

            Task.Delay(2000).Wait();

            Console.WriteLine("After");
        }
    }
}