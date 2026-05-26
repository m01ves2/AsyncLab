using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ex07.DeadlockIntuition
{
    public class SingleThreadSyncContext : SynchronizationContext
    {
        private readonly BlockingCollection<(SendOrPostCallback, object?)> _queue = new();

        public SingleThreadSyncContext()
        {
            var thread = new Thread(RunOnCurrentThread);
            thread.Start();
        }

        private void RunOnCurrentThread()
        {
            foreach (var work in _queue.GetConsumingEnumerable()) {
                work.Item1(work.Item2);
            }
        }

        public override void Post(SendOrPostCallback d, object? state)
        {
            _queue.Add((d, state));
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            SynchronizationContext.SetSynchronizationContext(new SingleThreadSyncContext());

            Console.WriteLine("Main start");

            var result = GetDataAsync().Result; //шаг1.→ блокируется

            Console.WriteLine(result);
        }

        private static async Task<string> GetDataAsync()
        {
            await Task.Delay(2000); //шаг2.→ продолжение планируется в SynchronizationContext
            return "OK"; //шаг3.→ пытается вернуться на Main thread, но Main уже заблокирован

            //ИТОГО
            //Thread A ждёт Task
            //Task ждёт Thread A
            //= DEADLOCK - “continuation wants to return to blocked thread”

            //РЕШЕНИЕ ПРОБЛЕМЫ
            //await Task.Delay(2000).ConfigureAwait(false);
            //return "OK";
            //шаг4.→ если сделать так то,
            //continuation НЕ идёт в SynchronizationContext
            //идёт в ThreadPool
            //deadlock исчезает

        }
    }
}