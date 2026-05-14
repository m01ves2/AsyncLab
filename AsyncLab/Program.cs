using System.Diagnostics.Metrics;

namespace AsyncLab
{
    public class Program
    {
        public static int counter = 0;
        public static void Main(string[] args)
        {
            Work();
        }

        public static void Work()
        {
            for (int i = 0; i < 1_000_000; i++) {
                counter++;
            }
        }
    }
}