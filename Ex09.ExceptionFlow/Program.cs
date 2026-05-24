namespace Ex09.ExceptionFlow
{
    public delegate Task RequestDelegate();
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //await Experiment1();

            //await Experiment2();

            await Experiment3();
        }


        public static async Task Experiment1()
        {
            var task = CalculateAsync(1);
            Logger.Log($"task.Status: {task.Status}");

            try {
                await task;
            }
            catch (Exception ex) {
                Logger.Log(ex.ToString());
            }
        }
        public static async Task CalculateAsync(int id)
        {
            await Task.Delay(1000);
            throw new Exception($"Failure {id}");
        }


        public static async Task Experiment2()
        {
            var tasks = new List<Task<int>>();

            for (int i = 1; i <= 5; i++) {
                tasks.Add(CrashAsync(i));
            }
            var combinedTask = Task.WhenAll(tasks);

            try {
                await combinedTask;
            }
            catch (Exception ex) {
                //Logger.Log(ex.Message); //можно увидеть, что в combinedTask аккумулировано сразу 2 Exceptions (id == 2 && id == 4)
                Logger.Log($"Caught: {ex.GetType().Name}");
                Logger.Log($"Message: {ex.Message}");
            }

            Logger.Log($"combinedTask.Exception type: {combinedTask.Exception.GetType().Name}");

            foreach (var ex in combinedTask.Exception.InnerExceptions) {
                Logger.Log($"Inner: {ex.Message}");
            }
        }
        private static async Task<int> CrashAsync(int id)
        {
            Logger.Log($"Task {id} started");
            await Task.Delay(500);

            if (id % 2 == 0) {
                Logger.Log($"Task {id} failed");
                throw new Exception($"Even failure: {id}");
            }

            Logger.Log($"Task {id} completed");
            return id;
        }


        private static async Task Experiment3()
        {
            //await ExperimentAwait();
            await ExperimentResult();
        }

        private static async Task ExperimentAwait()
        {
            Logger.Log("Before await");

            try {
                var task = CrashAsync(2);

                Logger.Log("Task created");

                await task;
            }
            catch (Exception ex) {
                Logger.Log($"Caught: {ex.Message}");
            }

            Logger.Log("After await");
        }

        private static async Task ExperimentResult()
        {
            Logger.Log("Before Result");

            try {
                var task = CrashAsync(2);

                Logger.Log("Task created");

                var x = task.Result;
            }
            catch (Exception ex) {
                Logger.Log($"Caught: {ex.GetType().Name}");
                Logger.Log($"Caught: {ex.Message}");
            }

            Logger.Log("After Result");
        }
    }
}