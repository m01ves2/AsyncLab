namespace Ex11.CancellationToken
{
    public delegate Task RequestDelegate();
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Experiment1();
        }


        public static async Task Experiment1()
        {

            CancellationTokenSource cts = new CancellationTokenSource();
            System.Threading.CancellationToken token = cts.Token;

            var t1 = WorkerAsync(1, token);
            var t2 = WorkerAsync(2, token);
            var t3 = WorkerAsync(3, token);


            await Task.Delay(2000);
            cts.Cancel();
            cts.Dispose();

            try {
                await Task.WhenAll(t1, t2, t3);
            }
            catch (OperationCanceledException) {
                Logger.Log("At least one task was canceled");
            }
            finally {
                Logger.Log($"t1: {t1.Status}");
                Logger.Log($"t2: {t2.Status}");
                Logger.Log($"t3: {t3.Status}");
            }
        }

        private static async Task<int> WorkerAsync(int id, System.Threading.CancellationToken token)
        {
            Logger.Log($"Worker {id} started");
            await Task.Delay(5000, token);

            Logger.Log($"Worker {id} finished");

            return id;
        }
    }
}