using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ex04.BlockingVsAsync
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Experiment1();
            await Experiment2();
        }

        private static void Experiment1()
        {
            Logger.Log($"Experiment1 started. [Thread {Thread.CurrentThread.ManagedThreadId}]");
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 20; i++) {
                int res = FakeDbCallAsync(i).Result;
            }
            sw.Stop();
            Logger.Log($"Experiment1 finished. Time [{DateTime.Now}] elapsed: {sw.ElapsedMilliseconds}");
        }

        private static async Task Experiment2()
        {
            Logger.Log($"Experiment2 started. [Thread {Thread.CurrentThread.ManagedThreadId}]");
            Stopwatch sw = Stopwatch.StartNew();
            List<Task<int>> tasks = new List<Task<int>>();
            for (int i = 0; i < 20; i++) {
                tasks.Add(FakeDbCallAsync(i));
            }
            await Task.WhenAll(tasks);
            Logger.Log($"Experiment2 continuation. [Thread {Thread.CurrentThread.ManagedThreadId}]");
            sw.Stop();
            Logger.Log($"Experiment2 finished. Time [{DateTime.Now}], elapsed: {sw.ElapsedMilliseconds}");
        }

        private static async Task<int> FakeDbCallAsync(int id)
        {
            Logger.Log($"FakeDbCallAsync start. [Thread {Thread.CurrentThread.ManagedThreadId}]");
            await Task.Delay(2000);
            Logger.Log($"FakeDbCallAsync continuation. [Thread {Thread.CurrentThread.ManagedThreadId}]");
            return id;
        }
    }
}