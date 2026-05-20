using System.Diagnostics;
using Ex01.SyncVsAsync;

namespace Ex01
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //1. usual sequential execution of code
            var fakeDb = new FakeDatabase();
            Stopwatch sw = Stopwatch.StartNew();
            fakeDb.GetUser();
            fakeDb.GetUser();
            fakeDb.GetUser();
            sw.Stop();
            Logger.Log($"Sync time: {sw.ElapsedMilliseconds} ms"); //~9sec

            //1. sequential execution of Task-based async code
            sw.Restart();
            await fakeDb.GetUserAsync();
            await fakeDb.GetUserAsync();
            await fakeDb.GetUserAsync();
            sw.Stop();
            Logger.Log($"Async sequential time: {sw.ElapsedMilliseconds} ms"); //~9sec

            sw.Restart();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 3; i++) {
                tasks.Add(fakeDb.GetUserAsync());
            }

            await Task.WhenAll(tasks);
            sw.Stop();
            Logger.Log($"Async WhenAll time: {sw.ElapsedMilliseconds} ms"); //~3sec
        }

    }
}