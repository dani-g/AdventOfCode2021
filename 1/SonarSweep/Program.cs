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
            var readings = await Input.GetReadings("input1");
            Console.WriteLine("1_1: " + CountDepthIncreases(readings));
            Console.WriteLine("-----------");
            Console.WriteLine("1_2: " + CountWindowDepthIncreases(readings));
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

        private static int CountWindowDepthIncreases(int[] depthReadings)
        {
            var windowReadingsSum = new List<int>();
            for (var i = 0; i < depthReadings.Length; i++)
            {
                var sum = depthReadings.Skip(i).Take(3).Sum();
                windowReadingsSum.Add(sum);
            }

            return CountDepthIncreases(windowReadingsSum.ToArray());
        }
    }
}
