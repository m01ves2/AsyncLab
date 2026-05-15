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

            DoCpuWork();

            sw.Stop();
            Console.WriteLine(counter);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms"); //2550 msec
        }

        public static void DoCpuWork()
        {
            double value = 0;

            for (int i = 0; i < 500_000_000; i++) {
                value += Math.Sqrt(i);
            }

            Console.WriteLine(value);
        }
    }
}