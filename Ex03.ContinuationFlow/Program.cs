namespace Ex03.ContinuationFlow
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var t1 = DoWorkAsync(1);
            var t2 = DoWorkAsync(2);

            await Task.WhenAll(t1, t2);
        }

        private static async Task DoWorkAsync(int id)
        {
            Logger.Log($"[{DateTime.Now:HH:mm:ss.fff}] [Thread {Thread.CurrentThread.ManagedThreadId}] {id} - Step 1");
            await Task.Delay(1000);

            Logger.Log($"[{DateTime.Now:HH:mm:ss.fff}] [Thread {Thread.CurrentThread.ManagedThreadId}] {id} - Step 2");
            await Task.Delay(1000);

            Logger.Log($"[{DateTime.Now:HH:mm:ss.fff}] [Thread {Thread.CurrentThread.ManagedThreadId}] {id} - Step 3");
        }
    }
}