using System.Diagnostics;
using System.Threading.Tasks;
using Ex02.TaskLifecycle;

namespace Ex02
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Experiment1();

            await Experiment2();
        }

        private static async Task Experiment1()
        {
            var task1 = CalculateAsync(1);
            var task2 = CalculateAsync(2);
            var task3 = CalculateAsync(3);

            Logger.Log("task1 status: " + task1.Status);
            Logger.Log("task2 status: " + task2.Status);
            Logger.Log("task3 status: " + task3.Status);

            List<Task<int>> tasks = new List<Task<int>>() { task1, task2, task3 };

            await Task.Delay(1000);
            int[] results = await Task.WhenAll(tasks);

            Logger.Log("task1 status: " + task1.Status);
            Logger.Log("task2 status: " + task2.Status);
            Logger.Log("task3 status: " + task3.Status);
        }

        private static async Task Experiment2()
        {
            Console.WriteLine("Before call");
            var task = CalculateAsync(99);
            Console.WriteLine("After call");
        }

        private static async Task<int> CalculateAsync(int id)
        {
            Logger.Log("Method started");
            await Task.Delay(2000);
            Logger.Log("After await");
            return id * 10;
        }
    }
}