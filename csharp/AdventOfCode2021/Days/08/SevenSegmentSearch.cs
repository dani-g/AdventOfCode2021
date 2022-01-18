using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
   internal class SevenSegmentSearch : IDayExercise
   {
      public int Order => 8;

      public string Name => "Day 8 - Seven Segment Search";

      public async Task<long> Solve1()
      {
         var processor = await SevenSegmentDisplaySignalProcessor.CreateSevenSegmentDisplaySignalProcessor();
         return processor.CountEasyDigits();
      }

      public async Task<long> Solve2()
      {
         var processor = await SevenSegmentDisplaySignalProcessor.CreateSevenSegmentDisplaySignalProcessor();
         var outputs = processor.DecodeOutputs();
         return outputs.Sum();
      }


      internal class SevenSegmentDisplaySignalProcessor
      {
         private readonly DisplaySignalSegment[] _segments;
         /*  Normal display
              0:      1:      2:      3:      4:
             aaaa    ....    aaaa    aaaa    ....
            b    c  .    c  .    c  .    c  b    c
            b    c  .    c  .    c  .    c  b    c
             ....    ....    dddd    dddd    dddd
            e    f  .    f  e    .  .    f  .    f
            e    f  .    f  e    .  .    f  .    f
             gggg    ....    gggg    gggg    ....

              5:      6:      7:      8:      9:
             aaaa    aaaa    aaaa    aaaa    aaaa
            b    .  b    .  .    c  b    c  b    c
            b    .  b    .  .    c  b    c  b    c
             dddd    dddd    ....    dddd    dddd
            .    f  e    f  .    f  e    f  .    f
            .    f  e    f  .    f  e    f  .    f
             gggg    gggg    ....    gggg    gggg
          */
         private readonly Dictionary<int, string> _displayNormalSignals = new()
         {
            [0] = "abcefg",
            [1] = "cf",
            [2] = "acdeg",
            [3] = "acdfg",
            [4] = "bcdf",
            [5] = "abdfg",
            [6] = "abdefg",
            [7] = "acf",
            [8] = "abcdefg",
            [9] = "abcdfg",
         };

         private SevenSegmentDisplaySignalProcessor(DisplaySignalSegment[] input)
         {
            _segments = input;
         }

         public static async Task<SevenSegmentDisplaySignalProcessor> CreateSevenSegmentDisplaySignalProcessor()
         {
            var input = await Input.ReadInput("inputs/input_8");
            var splitParts = input.Select(seg => {
               var parts = seg.Split(" | ");
               var patterns = parts[0].Split(' ');
               var output = parts[1].Split(' ');
               return new DisplaySignalSegment(patterns, output);
            });
            return new SevenSegmentDisplaySignalProcessor(splitParts.ToArray());
         }

         public int CountEasyDigits() => _segments.Aggregate(0, (acc, cur) => acc + cur.CountEasyDigitsFromOutput());

         public int[] DecodeOutputs() {
            var outputs = new List<int>();
            foreach (var segment in _segments)
            {
               var decodedSignals = DecodeDisplaySignals(segment.SignalPatterns);
               var output = new string[4];

               // first digit
               output[0] = GetDigit(segment, decodedSignals, 0);
               output[1] = GetDigit(segment, decodedSignals, 1);
               output[2] = GetDigit(segment, decodedSignals, 2);
               output[3] = GetDigit(segment, decodedSignals, 3);

               outputs.Add(int.Parse(string.Join("", output)));
            }
            return outputs.ToArray();
         }

         private string GetDigit(DisplaySignalSegment segment, Dictionary<int, string> decodedSignals, int digitPosition)
         {
            var firstDigitKey = segment.Output[digitPosition].SortChars();
            return decodedSignals.First(x => x.Value == firstDigitKey).Key.ToString();
         }

         private Dictionary<int, string> DecodeDisplaySignals(string[] signalPatterns)
         {
            var orderedPatterns = signalPatterns.Select(s => s.SortChars());
            var signals = new Dictionary<int, string>();
            signals[1] = orderedPatterns.First(s => s.Length == 2);
            signals[4] = orderedPatterns.First(s => s.Length == 4);
            signals[7] = orderedPatterns.First(s => s.Length == 3);
            signals[8] = orderedPatterns.First(s => s.Length == 7);

            signals[9] = orderedPatterns.First(s => s.Length == 6 && signals[4].All(c => s.Contains(c)));
            signals[0] = orderedPatterns.First(s => s.Length == 6 && signals[9] != s && signals[1].All(c => s.Contains(c)));
            signals[6] = orderedPatterns.First(s => s.Length == 6 && signals[9] != s && signals[0] != s);

            signals[3] = orderedPatterns.First(s => s.Length == 5 && signals[1].All(c => s.Contains(c)));
            signals[5] = orderedPatterns.First(s => s.Length == 5 && s != signals[3] && s.All(c => signals[6].Contains(c)));
            signals[2] = orderedPatterns.First(s => s.Length == 5 && s != signals[3] && s != signals[5]);


            return signals;
         }

         internal record DisplaySignalSegment(string[] SignalPatterns, string[] Output)
         {
            internal int CountEasyDigitsFromOutput() 
               => Output.Count(o => o.Length == 2) + Output.Count(o => o.Length == 3) + Output.Count(o => o.Length == 4) + Output.Count(o => o.Length == 7);
         };
      }
   }

   internal static class StringExtensions
   {
      public static string SortChars(this string input)
      {
         var arr = input.ToCharArray();
         Array.Sort(arr, StringComparer.OrdinalIgnoreCase);
         return string.Join("", arr);
      }
   }
}
