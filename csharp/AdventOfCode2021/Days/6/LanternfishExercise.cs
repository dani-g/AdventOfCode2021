using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
   internal partial class LanternfishExercise : IDayExercise
   {
      public int Order => 6;

      public string Name => "Day 6 - Lanternfish";

      
      public async Task<long> Solve1()
      {
         var population = await LanternfishPopulationEfficient.GetFreshPopulationAsync();
         population.AdvanceDays(80);
         return (int)population.PopulationSize;
      }

      public async Task<long> Solve2()
      {
         //return Task.FromResult(0);
         var population = await LanternfishPopulationEfficient.GetFreshPopulationAsync();
         population.AdvanceDays(256);
         return population.PopulationSize;
      }
   }
}
