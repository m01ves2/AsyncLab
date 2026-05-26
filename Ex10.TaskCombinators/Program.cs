namespace Ex10.TaskCombinators
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var t1 = WorkerAsync(1, 3000);
            var t2 = WorkerAsync(2, 1000);
            var t3 = WorkerAsync(3, 2000);

            var winner = await Task.WhenAny(t1, t2, t3); //returns Task<Task<int>>
            int result = await winner; //returns int
            Logger.Log($"WinnerId:{result}");

            Logger.Log($"t1: {t1.Status}");
            Logger.Log($"t2: {t2.Status}");
            Logger.Log($"t3: {t3.Status}");
        }

        private static async Task<int> WorkerAsync(int id, int delay)
        {
            Logger.Log($"Worker {id} started");

            await Task.Delay(delay);

            Logger.Log($"Worker {id} finished");

            return id;
        }
    }
}