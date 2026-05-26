using System.Diagnostics;
using System.Threading.Tasks;

namespace Ex06.ThreadPoolStarvation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Experiment1();
            await Experiment2();
        }

        private static async Task Experiment1() //GOOD SERVER
        {
            Stopwatch sw = Stopwatch.StartNew();

            List<Task<int>> tasks = new List<Task<int>>();
            for(int i = 0; i < 100; i++) {
                tasks.Add(FakeRequestAsync(i));
            }

            await Task.WhenAll(tasks);

            sw.Stop();
            Logger.Log($"Experiment1 elapsed: {sw.ElapsedMilliseconds}");
        }


        private static async Task Experiment2() //BAD SERVER
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<Task> tasks = new();
            for (int i = 0; i < 100; i++) {
                //некорректное поведение, поскольку i = 100 всегда.
                //tasks.Add(Task.Run(() =>
                //{
                //    var res = FakeRequestAsync(i).Result;
                //}));

                int requestId = i; //нужно сохранить i, чтобы i не была равна 100. variable different for each iteration.

                tasks.Add(Task.Run(() =>
                {
                    var res = FakeRequestAsync(requestId).Result; //потоки занимаются и блокируются тут. ThreadPool starvation
                    //либо можно еще вот так:
                    //FakeRequestAsync(requestId).Wait();
                }));
            }

            await Task.WhenAll(tasks);

            sw.Stop();
            Logger.Log($"Experiment2 elapsed: {sw.ElapsedMilliseconds}");
        }

        private static async Task<int> FakeRequestAsync(int id)
        {
            Logger.Log($"Request {id} started");
            await Task.Delay(2000);
            Logger.Log($"Request {id} finished");
            return id;
        }
    }
}