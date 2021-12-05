using System;
using System.Threading.Tasks;

namespace Dive
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var readings = await Input.GetReadings("input_2");
            var (horizontal, depth) =  CalculateFinalPosition(readings);
            Console.WriteLine($"({horizontal}, {depth})");
            Console.WriteLine(horizontal * depth);
            Console.Read();
        }

        private static (int Horizontal, int Depth) CalculateFinalPosition(string[] commands)
        {
            var submarine = new Submarine();

            foreach (var command in commands)
            {
                var splitCommand = command.Split(" ");
                submarine.MoveWithAim(splitCommand[0], int.Parse(splitCommand[1]));
            }
            return (submarine.Horizontal, submarine.Depth);
        }
    }
}
