namespace Ex12.TimeoutPatterns
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Experiment1();
        }


        public static async Task Experiment1()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t1 = WorkerAsync(1, 3000, token);
            var t2 = WorkerAsync(2, 1000, token);
            var t3 = WorkerAsync(3, 5000, token);

            var allTasks = Task.WhenAll(t1, t2, t3);
            var timeout = Task.Delay(2000);

            var completed = await Task.WhenAny(allTasks, timeout); //!!!

            if (completed == timeout) {
                Logger.Log("TIMEOUT reached");

                cts.Cancel(); // сигнал воркерам остановиться
            }
            else {
                Logger.Log("ALL tasks completed in time");
            }

            try {
                await allTasks; // добираем результат или исключение
            }
            catch (OperationCanceledException) {
                Logger.Log("Tasks canceled due to timeout");
            }
        }

      
        private static async Task<int> WorkerAsync(int id, int delay, System.Threading.CancellationToken token)
        {
            Logger.Log($"Worker {id} started");
            await Task.Delay(delay, token);
            Logger.Log($"Worker {id} finished");

            return id;
        }

    }
}