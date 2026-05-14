using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static int counter = 0;
        public static void Main(string[] args)
        {
            //Work();
            Thread t1 = new Thread(Work);
            Thread t2 = new Thread(Work);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        public static void Work()
        {
            for (int i = 0; i < 1_000_000; i++) {
                counter++;
            }
        }
    }
}