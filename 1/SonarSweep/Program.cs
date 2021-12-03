using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SonarSweep
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(CountDepthIncreases(await Input.GetReadings()));
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static int CountDepthIncreases(int[] depthReadings)
        {
            var countIncreases = 0;
            var _ = depthReadings.Aggregate(int.MaxValue, (prev, next) =>
            {
                if (next > prev) countIncreases++;
                return next;
            });
            return countIncreases;
        }
    }
}
