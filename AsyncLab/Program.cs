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

            Thread t1 = new Thread(Work);
            Thread t2 = new Thread(Work);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();



            // важно: даём ThreadPool время завершить работу
            Thread.Sleep(2000);


            sw.Stop();
            Console.WriteLine(counter);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms"); //3100 msec
        }

        public static void Work()
        {
            for (int i = 0; i < 50_000_000; i++) {
                Interlocked.Increment(ref counter);
            }
        }
    }
}