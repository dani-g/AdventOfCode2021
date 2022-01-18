using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class SonarSweep : IDayExercise
    {
        public int Order => 1;

        public string Name => "Day 1 - Sonar Sweep";

        public async Task<long> Solve1()
        {
            var readings = await GetReadings();
            return CountDepthIncreases(readings);
        }



        public async Task<long> Solve2()
        {
            var readings = await GetReadings();
            return CountWindowDepthIncreases(readings);
        }

        private async Task<int[]> GetReadings()
            => (await Input.ReadInput("inputs/input_1"))
                    .Select(int.Parse)
                    .ToArray();

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
