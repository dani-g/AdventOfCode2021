using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
   internal class BingoInputReader
   {
      internal async Task<Bingo> BuildBingoGame(string filename)
      {
         try
         {
            using var sr = new StreamReader(filename);
            var numberLine = await sr.ReadLineAsync();
            var numbersToDraw = numberLine.Split(',').Select(ushort.Parse).ToArray();

            var boards = new List<Board>();
            while(sr.ReadLine() != null)
            {
               var boardLines = new string[Board.BOARD_SIZE];
               for(int i = 0; i < Board.BOARD_SIZE; i++)
               {
                  boardLines[i] = await sr.ReadLineAsync();
                  if (sr.EndOfStream)
                  {
                     break;
                  }
               }
               boards.Add(new Board(boardLines));
            }
            return new Bingo(numbersToDraw, boards);
         }
         catch (FileNotFoundException ex)
         {
            throw new Exception("Failed to read the file", ex);
         }

      }

      private async Task<string[]> ReadInput(string filename)
      {
         try
         {
            using var sr = new StreamReader(filename);
            var input = await sr.ReadToEndAsync();

            return input.Split(Environment.NewLine)
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();
         }
         catch (FileNotFoundException ex)
         {
            throw new Exception("Failed to read the file", ex);
         }
      }
   }
}
