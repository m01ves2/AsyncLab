namespace Ex08.FakePipeline
{
    public delegate Task RequestDelegate();
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await LoggingMiddleware(
                    () => AuthMiddleware(
                        () => EndpointHandler()
                        )
                  );
        }

        private static async Task LoggingMiddleware(RequestDelegate next)
        {
            Logger.Log("Before logging");
            Logger.Log($"[Thread {Thread.CurrentThread.ManagedThreadId}]");
            await Task.Delay(1000);
            await next();
            Logger.Log($"[Thread {Thread.CurrentThread.ManagedThreadId}]");
            Logger.Log("After logging");
        }

        private static async Task AuthMiddleware(RequestDelegate next)
        {
            Logger.Log("Before auth");
            Logger.Log($"[Thread {Thread.CurrentThread.ManagedThreadId}]");
            await Task.Delay(1000);
            await next();
            Logger.Log($"[Thread {Thread.CurrentThread.ManagedThreadId}]");
            Logger.Log("After auth");
        }

        private static async Task EndpointHandler()
        {
            Logger.Log("Before endpoint");
            Logger.Log($"[Thread {Thread.CurrentThread.ManagedThreadId}]");
            Logger.Log("Endpoint processing");
            await Task.Delay(1000);
            Logger.Log($"[Thread {Thread.CurrentThread.ManagedThreadId}]");
            Logger.Log("After endpoint");
        }

    }
}