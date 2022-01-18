using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class BinaryDiagnostics : IDayExercise
    {
        public int Order => 3;

        public string Name => "Day 3 - Binary Diagnostics";

        public async Task<long> Solve1()
        {
            var input = await Input.ReadInput("inputs/input_3");

            var numberOfInputs = input.Length;
            var inputValueSize = input.First().Length;
            var gammaRate = new char[inputValueSize];
            var epsilonRate = new char[inputValueSize];
            for (var i = 0; i < inputValueSize; i++)
            {
                var sum = input.Select(v => int.Parse(v[i].ToString())).Sum();
                epsilonRate[i] = sum > (numberOfInputs / 2) ? '0' : '1';
                gammaRate[i] = sum > (numberOfInputs / 2) ? '1' : '0';
            }
            var gammaValue = Convert.ToInt32(new string(gammaRate), 2);
            var epsilonValue = Convert.ToInt32(new string(epsilonRate), 2);

            return gammaValue * epsilonValue;
        }

        public async Task<long> Solve2()
        {
            var input = await Input.ReadInput("inputs/input_3");

            var numberOfInputs = input.Length;
            var inputValueSize = input.First().Length;
            var oxygenGeneratorRating = new List<string>(input);
            var co2GeneratorRating = new List<string>(input);

            for (ushort i = 0; i < inputValueSize; i++)
            {
                if (oxygenGeneratorRating.Count > 1)
                {
                    var oxygenSum = oxygenGeneratorRating.Select(v => int.Parse(v[i].ToString())).Sum();
                    var oxigenMostCommon = oxygenSum >= (oxygenGeneratorRating.Count / 2.0d) ? '1' : '0';
                    oxygenGeneratorRating.RemoveAll(val => val[i] != oxigenMostCommon);
                }

                if (co2GeneratorRating.Count > 1)
                {
                    var co2Sum = co2GeneratorRating.Select(v => int.Parse(v[i].ToString())).Sum();
                    var co2MostCommon = co2Sum >= (co2GeneratorRating.Count / 2.0d) ? '1' : '0';
                    co2GeneratorRating.RemoveAll(val => val[i] == co2MostCommon);
                }
            }
            var o2Value = Convert.ToInt32(oxygenGeneratorRating.First(), 2);
            var co2Value = Convert.ToInt32(co2GeneratorRating.First(), 2);

            return (int)(o2Value * co2Value);
        }
    }
}
