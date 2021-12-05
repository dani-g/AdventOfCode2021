using System;
using System.Collections.Generic;

namespace Dive
{
    public class Submarine
    {
        public int Horizontal { get; set; } = 0;
        public int Depth { get; set; } = 0;
        public int Aim { get; set; } = 0;

        public void ResetPosition()
        {
            Horizontal = Depth = Aim = 0;
        }

        public void Move(string direction, int distance)
        {
            switch (direction)
            {
                case "forward":
                    Horizontal += distance;
                    break;
                case "up":
                    Depth -= distance;
                    break;
                case "down":
                    Depth += distance;
                    break;
                default:
                    throw new Exception($"direction not recognized {direction}");
            }
        }

        public void MoveWithAim(string direction, int distance)
        {
            switch (direction)
            {
                case "forward":
                    Horizontal += distance;
                    Depth += Aim * distance;
                    break;
                case "up":
                    Aim -= distance;
                    break;
                case "down":
                    Aim += distance;
                    break;
                default:
                    throw new Exception($"direction not recognized {direction}");
            }
        }

    }
}