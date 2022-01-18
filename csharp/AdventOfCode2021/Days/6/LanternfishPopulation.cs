using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
   internal partial class LanternfishExercise
   {
      internal class LanternfishPopulation
      {
         internal static async Task<LanternfishPopulation> GetFreshPopulationAsync() {
            var input = await Input.ReadInput("inputs/input_6");
            if (input.Length != 1) throw new Exception("Input incorrect. Should be just 1 line");

            var population = input[0].Split(',').Select(ushort.Parse).ToList();
            return new LanternfishPopulation(population);
         }

         private const ushort NEW_LANTERNFISH_INITIAL_VALUE = 8;
         private const ushort LANTERNFISH_RESET_VALUE = 6;
         private List<ushort> _population;

         private LanternfishPopulation(List<ushort> initialPopulation)
         {
            _population = initialPopulation;
         }

         public long PopulationSize => _population.Count;

         public void AdvanceDay()
         {
            var newLanternfish = new List<ushort>();
            for (int i = 0; i < _population.Count; i++)
            {
               var lanternFishValue = _population[i];
               if(lanternFishValue > 0)
               {
                  _population[i]--;
               }
               else
               {
                  newLanternfish.Add(NEW_LANTERNFISH_INITIAL_VALUE);
                  _population[i] = LANTERNFISH_RESET_VALUE;
               }
            }
            _population.AddRange(newLanternfish);
         }

         public void AdvanceDays(int numberOfDays)
         {
            for(int i = 0; i < numberOfDays; i++)
            {
               AdvanceDay();
            }
         }
      }
   }

   internal class LanternfishPopulationEfficient
   {
      internal static async Task<LanternfishPopulationEfficient> GetFreshPopulationAsync()
      {
         var input = await Input.ReadInput("inputs/input_6");
         if (input.Length != 1) throw new Exception("Input incorrect. Should be just 1 line");

         var population = input[0].Split(',').Select(ushort.Parse).ToList();
         return new LanternfishPopulationEfficient(population);
      }

      private const ushort MAX_LANTERNFISH_INITIAL_VALUE = 8;
      private const ushort LANTERNFISH_RESET_VALUE = 6;
      private Dictionary<ushort, long> _populationGrouped;

      private LanternfishPopulationEfficient(IList<ushort> population)
      {
         _populationGrouped = population
            .GroupBy(l => l)
            .ToDictionary(gr => gr.Key, gr => gr.LongCount());
      }

      public long PopulationSize => _populationGrouped.Values.Sum();

      public void AdvanceDay()
      {
         var newPopulationGrouped = new Dictionary<ushort, long>();

         for (ushort i = 0; i <= MAX_LANTERNFISH_INITIAL_VALUE; i++)
         {
            var numberOfFish = _populationGrouped.GetValueOrDefault(i);
            if (i == 0)
            {
               newPopulationGrouped[MAX_LANTERNFISH_INITIAL_VALUE] = numberOfFish;
               newPopulationGrouped[LANTERNFISH_RESET_VALUE] = numberOfFish;
            }
            else
            {
               newPopulationGrouped[(ushort)(i - 1)] = numberOfFish + newPopulationGrouped.GetValueOrDefault((ushort)(i - 1));
            }
         }
         _populationGrouped = newPopulationGrouped;
      }

      public void AdvanceDays(int numberOfDays)
      {
         for (int i = 0; i < numberOfDays; i++)
         {
            AdvanceDay();
         }
      }
   }
}
