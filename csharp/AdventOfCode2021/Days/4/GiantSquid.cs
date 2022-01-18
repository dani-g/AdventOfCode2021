using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
   public class GiantSquid : IDayExercise
   {
      private readonly BingoInputReader _bingoInputReader = new BingoInputReader();
      public int Order => 4;

      public string Name => "Day 4 - Giant Squid";

      public async Task<long> Solve1()
      {
         var bingo = await _bingoInputReader.BuildBingoGame("inputs/input_4");
         return bingo.GetWinningScore();
      }

      public async Task<long> Solve2()
      {
         var bingo = await _bingoInputReader.BuildBingoGame("inputs/input_4");
         return bingo.GetLastWinningScore();
      }
   }
}
