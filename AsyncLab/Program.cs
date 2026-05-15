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

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 2; i++) {
                Thread t = new Thread(Work);
                threads.Add(t);
                t.Start();
            }

            foreach (Thread t in threads) {
                t.Join();
            }


            // важно: даём ThreadPool время завершить работу
            Thread.Sleep(2000);


            sw.Stop();
            Console.WriteLine(counter);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms"); //2550 msec
        }

        public static void Work()
        {
            for (int i = 0; i < 50_000_000; i++) {
                //Interlocked.Increment(ref counter);
                counter++;
            }
        }
    }
}