using System.Diagnostics;

namespace Ex14.LoadSimulation
{
    public delegate Task RequestDelegate(Request request);
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //await Experiment1();
            await Experiment2();
        }

        private static async Task Experiment1()
        {
            List<Task> tasks = new List<Task>();
            Stopwatch sw = Stopwatch.StartNew();
            for(int i = 0; i < 100; i++) {
                tasks.Add(ProcessRequest(new Request() { Id = i}));
            }
            await Task.WhenAll(tasks);
            sw.Stop();
            Logger.Log($"Async Load Experiment. time elapsed: {sw.ElapsedMilliseconds}"); //~1050 ms
        }
        private static async Task ProcessRequest(Request req)
        {
            await LoggingMiddleware(req,
                    async (req) =>
                    {
                        await AuthMiddleware(
                            req,
                            EndpointHandler
                            );
                    });
        }

        private static async Task Experiment2()
        {
            List<Task> tasks = new List<Task>();
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < 100; i++) {
                tasks.Add(ProcessBlockingRequest(new Request() { Id = i }));
            }
            await Task.WhenAll(tasks);
            sw.Stop();
            Logger.Log($"Async Load Experiment. time elapsed: {sw.ElapsedMilliseconds}"); //~50 ms
        }

        private static async Task ProcessBlockingRequest(Request req)
        {
            await LoggingMiddleware(req,
                    async (req) =>
                    {
                        await AuthMiddleware(
                            req,
                            EndpointBlockingHandler
                            );
                    });
        }

        private static async Task LoggingMiddleware(Request req, RequestDelegate next)
        {
            Logger.Log($"[Request {req.Id}] Before logging");
            await next(req);
            Logger.Log($"[Request {req.Id}] After logging");
        }

        private static async Task AuthMiddleware(Request req, RequestDelegate next)
        {
            Logger.Log($"[Request {req.Id}] Before auth");
            await next(req);
            Logger.Log($"[Request {req.Id}] After auth");
        }

        private static async Task EndpointHandler(Request req)
        {
            Logger.Log($"[Request {req.Id}] Before endpoint");
            Logger.Log($"Request {req.Id} processing");
            await Task.Delay(1000);
            Logger.Log($"[Request {req.Id}] After endpoint");
        }

        private static async Task EndpointBlockingHandler(Request req)
        {
            Logger.Log($"[Request {req.Id}] Before endpoint");
            Logger.Log($"[Request {req.Id}] processing. Thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Logger.Log($"[Request {req.Id}] After endpoint");
        }
    }
}