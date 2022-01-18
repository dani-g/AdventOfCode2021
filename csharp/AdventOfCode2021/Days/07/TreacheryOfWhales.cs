using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
   internal class TreacheryOfWhales : IDayExercise
   {
      public int Order => 7;

      public string Name => "Day 7 - The Treachery of Whales";

      public async Task<long> Solve1()
      {
         var exercise = await CrabSubmarinesExercise.CreateCrabSubmarinesExercise();
         var result = await exercise.FindLeastFuelPosition();
         return result.fuelCost;
      }

      public async Task<long> Solve2()
      {
         var exercise = await CrabSubmarinesExercise.CreateCrabSubmarinesExercise();
         var result = await exercise.FindLeastFuelPosition2();
         return result.fuelCost;
      }

      internal class CrabSubmarinesExercise
      {
         internal static async Task<CrabSubmarinesExercise> CreateCrabSubmarinesExercise()
         {
            var input = await Input.ReadInput("inputs/input_7");
            if (input.Length != 1) throw new Exception("Input incorrect. Should be just 1 line");

            var subs = input[0].Split(',').Select(int.Parse).ToArray();
            return new CrabSubmarinesExercise(subs);
         }

         private readonly int[] _crabPositions;

         private CrabSubmarinesExercise(int[] positions)
         {
            _crabPositions = positions;
         }

         public async Task<(int position, int fuelCost)> FindLeastFuelPosition()
         {
            // use avg as starting point
            //var bestPosition = (int)Math.Round(_crabPositions.Average());
            var max = _crabPositions.Max();
            //var fuelCost = await CalculateFuelForPosition(bestPosition);
            var tasks = new List<Task<(int position, int fuelCost)>>();
            for (var i = 1; i < max; i++)
            {
               tasks.Add(CalculateFuelForPosition(i));
            }
            var results = (await Task.WhenAll(tasks)).OrderBy(x => x.fuelCost).ThenBy(x => x.position);
            return results.First();
            //var counts = _crabPositions.GroupBy(l => l).ToDictionary(gr => gr.Key, gr => gr.Count()).OrderByDescending(c => c.Value).ToArray();
            //return bestPosition;
         }

         public async Task<(int position, int fuelCost)> FindLeastFuelPosition2()
         {
            var max = _crabPositions.Max();
            var tasks = new List<Task<(int position, int fuelCost)>>();
            for (var i = 1; i < max; i++)
            {
               tasks.Add(CalculateFuelForPosition2(i));
            }
            var results = (await Task.WhenAll(tasks)).OrderBy(x => x.fuelCost).ThenBy(x => x.position);
            return results.First();
         }

         private Task<(int position, int fuelCost)> CalculateFuelForPosition(int position) 
            => Task.Run(() => (position, _crabPositions.Aggregate(0, (acc, val) => acc + Math.Abs(val - position))));

         private Task<(int position, int fuelCost)> CalculateFuelForPosition2(int position)
            => Task.Run(() => (position, _crabPositions.Aggregate(0, (acc, val) =>
            {
               var moves = Math.Abs(val - position);
               var cost = 0;
               for(var i = 1; i <= moves; i++)
               {
                  cost += i;
               }
               return acc + cost;
            })));
      }
   }
}
