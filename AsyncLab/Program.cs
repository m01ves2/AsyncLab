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

            for (int i = 0; i < 100; i++) {
                Thread t = new Thread(DoWaitingWork);
                threads.Add(t);
                t.Start();
            }

            foreach (Thread t in threads) {
                t.Join();
            }


            sw.Stop();
            Console.WriteLine(counter);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms"); //3000 msec
        }

        public static void DoWaitingWork()
        {
            Thread.Sleep(3000);
        }
    }
}