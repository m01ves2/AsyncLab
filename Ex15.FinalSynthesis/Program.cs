using System.Text.Json;
using Ex15.FinalSynthesis.Dtos;

namespace Ex15.FinalSynthesis
{
    public delegate Task<DashboardResponse> RequestDelegate(Request request);
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var json = await Experiment1();

            Console.WriteLine(json); //иммитация получения json клиентом
        }

        private static async Task<string> Experiment1()
        {
            return await ProcessRequest(new Request() { Id = 1 });
        }
        private static async Task<string> ProcessRequest(Request req)
        {
            return await RequestPipeline(req);
        }

        private static async Task<string> RequestPipeline(Request req)
        {
            req = await LoggingMiddleware(req);
            req = await AuthMiddleware(req);

            var response = await EndpointHandler(req);

            return await ResponseMiddleware(response);
        }

        private static async Task<Request> LoggingMiddleware( Request req)
        {
            Logger.Log($"[Request {req.Id}] Before logging");

            await Task.Delay(100); // имитация IO (логирование, tracing, etc)

            Logger.Log($"[Request {req.Id}] After logging");

            return req;
        }
        private static async Task<Request> AuthMiddleware(Request req)
        {
            Logger.Log($"[Request {req.Id}] Auth check started");

            await Task.Delay(150); // имитация DB / cache / token validation

            bool isValid = true; // допустим, проверили токен

            if (!isValid)
                throw new Exception("Unauthorized");

            Logger.Log($"[Request {req.Id}] Auth OK");

            return req;
        }

        private static async Task<DashboardResponse> EndpointHandler(Request req)
        {
            //1. IO-bound, DB. async решает IO waiting. делаем через async. считаем, что это независимые задачи, авторизация была раньше,
            // а значит, можно запускать параллельно ассинхронно
            var profileTask = LoadProfileAsync(req);
            var ordersTask = LoadOrdersAsync(req);

            //ожидаем выполнение тасков
            //List<Task> tasks = new List<Task>() { profileTask, ordersTask }; // так было раньше в exercises, но
            await Task.WhenAll(profileTask, ordersTask); //здесь сделано не через List, потому что данные возвращаются разных типов
            
            //получаем результаты работы. они разных типов, поэтому в разных переменных, не массиве
            var profile = await profileTask;
            var orders = await ordersTask;

            //2. PU processing. здесь идёт обработка данных, тяжёлые операции, например,
            //total spent, average order, sorting, grouping, recommendation score, analytics, filtering, ranking
            //2.1 просто синхронно
            var stats = ComputeStatistics(profile, orders);
            //либо
            //2.2 асинхронно
            //var stats = await Task.Run(() => ComputeStatistics(profile, orders));

            //3. рекоммендации с посторонних серверов, с Canceletion по timeout
            List<Recommendation> recommendations = [];
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var recommendationsTask = GetRecommendationsAsync(token);
            var timeoutTask = Task.Delay(2000);
            var completed = await Task.WhenAny(recommendationsTask, timeoutTask);
            if (completed == timeoutTask) {
                Logger.Log("TIMEOUT reached");
                cts.Cancel(); // сигнал воркерам остановиться
            }
            
            try {
                recommendations = await recommendationsTask; // добираем результат или исключение
            }
            catch (OperationCanceledException) {
                Logger.Log("Recomendation Task canceled due to timeout");
            }

            var response = GetResponse(profile, orders, stats, recommendations);
            return response;
        }

        private static async Task<Profile> LoadProfileAsync(Request request)
        {
            await Task.Delay(2000); //получение данных из базы
            return new Profile() { 
                Id = request.Id, 
                Name = "User's Name" 
            };
        }

        private static async Task<List<Order>> LoadOrdersAsync(Request request)
        {
            await Task.Delay (2000);
            return new List<Order>() { 
                new Order() { Price = 1000 }, 
                new Order() { Price = 2000 }, 
                new Order() { Price = 5400 },
                new Order() { Price = 1640 },
            };
        }

        private static Statistics ComputeStatistics(Profile profile, List<Order> orders)
        {
            Thread.Sleep(3000);

            decimal sum = 0;
            orders.ForEach( o => sum += o.Price);

            return new Statistics() { OrdersCount = orders.Count, TotalSpent = sum };
        }

        private static async Task<List<Recommendation>> GetRecommendationsAsync(CancellationToken token)
        {
            await Task.Delay(1000, token);
            return new List<Recommendation>() {
                new Recommendation() { ProductName = "product-1" },
                new Recommendation() { ProductName = "product-2" },
                new Recommendation() { ProductName = "product-3" },
                new Recommendation() { ProductName = "product-4" },
                new Recommendation() { ProductName = "product-5" },
            };
        }

        private static DashboardResponse GetResponse(Profile profile, List<Order> orders, Statistics statistics, List<Recommendation> recommendations )
        {
            var response = new DashboardResponse()
            {
                Orders = orders,
                Profile = profile,
                Statistics = statistics,
                Recommendations = recommendations
            };
            return response;
        }

        private static Task<string> ResponseMiddleware(DashboardResponse response)
        {
            // serialization stage
            var json = JsonSerializer.Serialize(response); //это иммитация Kestrel, которая отправляет json клиенту

            Logger.Log("Response serialized");

            return Task.FromResult(json);
        }

    }
}