using System.Diagnostics;

namespace Ex05.BoundVsAsync
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Experiment1();
            await Experiment2();
            await Experiment3();
        }

        private static void Experiment1()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var res = HeavyCalculation(1);
            res = HeavyCalculation(2);
            res = HeavyCalculation(3);
            sw.Stop();
            Logger.Log($"Experiment1 elapsed: {sw.ElapsedMilliseconds}");
        }

        private static async Task Experiment2()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var task2 = Task.Run(() => HeavyCalculation(4));
            var task3 = Task.Run(() => HeavyCalculation(5));
            var task4 = Task.Run(() => HeavyCalculation(6));
            List<Task<long>> tasks = new List<Task<long>>() { task2 , task3 , task4 };
            
            await Task.WhenAll(tasks);
            sw.Stop();
            Logger.Log($"Experiment2 elapsed: {sw.ElapsedMilliseconds}");
        }

        private static async Task Experiment3()
        {
            Stopwatch sw = Stopwatch.StartNew();
            await FakeAsyncCalculation(7);
            sw.Stop();
            Logger.Log($"Experiment3 elapsed: {sw.ElapsedMilliseconds}");
        }

        private static long HeavyCalculation(int id)
        {
            Logger.Log($"Calculation {id} started");

            long sum = 0;

            for (long i = 0; i < 2_000_000_000; i++) {
                sum += i % 3;
            }

            Logger.Log($"Calculation {id} finished");

            return sum;
        }

        private static async Task<long> FakeAsyncCalculation(int id)
        {
            Logger.Log($"Calculation {id} started");

            long sum = 0;

            for (long i = 0; i < 2_000_000_000; i++) {
                sum += i % 3;
            }

            Logger.Log($"Calculation {id} finished");
            return sum;
        }
    }
}