namespace Ex13.RequestLifecycle
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
            var req = new Request() { Id = 1 };
            await ProcessRequest(req);
        }

        private static async Task Experiment2()
        {
            var tasks = new List<Task>();

            for (int i = 1; i <= 5; i++) {
                var req = new Request { Id = i };
                tasks.Add(ProcessRequest(req));
            }

            await Task.WhenAll(tasks);
        }

        private static async Task ProcessRequest(Request req)
        {
            //stage 1 - composition
            //LoggingMiddleware(req, AuthMiddleware(req, EndpointHandler(req)));

            //stage 2 - lambda
            //await LoggingMiddleware(req,
            //    (req) => AuthMiddleware(req,
            //    (req) => EndpointHandler(req)));

            //stage 2+
            //await LoggingMiddleware(req,
            //    (req) => AuthMiddleware(req, EndpointHandler));

            //stage 3 - middleware
            await LoggingMiddleware(req,
                    async (req) =>
                    {
                        await AuthMiddleware(
                            req,
                            EndpointHandler
                            );
                    });
        }

        private static async Task LoggingMiddleware(Request req, RequestDelegate next)
        {
            Logger.Log($"[Request {req.Id}] Before logging");
            await Task.Delay(1000);
            await next(req);
            Logger.Log($"[Request {req.Id}] After logging");
        }

        private static async Task AuthMiddleware(Request req, RequestDelegate next)
        {
            Logger.Log($"[Request {req.Id}] Before auth");
            await Task.Delay(1000);
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

    }
}