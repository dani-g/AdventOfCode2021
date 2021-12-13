using System;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Dive : IDayExercise
    {
        public int Order => 2;

        public string Name => "Day 2 - Dive";

        public async Task<int> Solve1()
        {
            var commands = await Input.ReadInput("inputs/input_2");
            var submarine = new Submarine();
            foreach (var command in commands)
            {
                var splitCommand = command.Split(" ");
                submarine.Move(splitCommand[0], int.Parse(splitCommand[1]));
            }
            Console.WriteLine($"(hor: {submarine.Horizontal}, depth: {submarine.Depth})");
            return submarine.Horizontal * submarine.Depth;
        }

        public async Task<int> Solve2()
        {
            var commands = await Input.ReadInput("inputs/input_2");
            var submarine = new Submarine();
            foreach (var command in commands)
            {
                var splitCommand = command.Split(" ");
                submarine.MoveWithAim(splitCommand[0], int.Parse(splitCommand[1]));
            }
            Console.WriteLine($"(hor: {submarine.Horizontal}, depth: {submarine.Depth})");
            return submarine.Horizontal * submarine.Depth;
        }
    }
}
