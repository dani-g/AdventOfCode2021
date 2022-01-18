using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
   internal class HydrothermalVenture : IDayExercise
   {
      public int Order => 5;

      public string Name => "Day 5 - Hydrothermal Venture";

      public async Task<long> Solve1()
      {
         var input = await Input.ReadInput("inputs/input_5");
         var pointCount = new Dictionary<Point, int>();
         foreach(var line in input)
         {
            var (start, end) = ParseLine(line);
            var points = GetLinePoints(start, end, false);
            foreach(var point in points) {
               pointCount[point] = pointCount.TryGetValue(point, out var pointValue) ? ++pointValue : 1;
            }
         }
         return pointCount.Values.Count(val => val > 1);
      }

      public async Task<long> Solve2()
      {
         var input = await Input.ReadInput("inputs/input_5");
         var pointCount = new Dictionary<Point, int>();
         foreach (var line in input)
         {
            var (start, end) = ParseLine(line);
            var points = GetLinePoints(start, end, true);
            foreach (var point in points)
            {
               pointCount[point] = pointCount.TryGetValue(point, out var pointValue) ? ++pointValue : 1;
            }
         }
         return pointCount.Values.Count(val => val > 1);
      }

      private Point[] GetLinePoints(Point start, Point end, bool useDiagonal)
      {
         // only supports vertical, horizontal and 45 degrees lines
         // vertical
         if(start.X == end.X)
         {
            var rangeStart = start.Y < end.Y ? start.Y : end.Y;
            var rangeEnd = start.Y < end.Y ? end.Y : start.Y;
            return Enumerable.Range(rangeStart, rangeEnd - rangeStart + 1)
               .Select(val => new Point(start.X, val))
               .ToArray();
         }
         // horizontal
         if(start.Y == end.Y)
         {
            var rangeStart = start.X < end.X ? start.X : end.X;
            var rangeEnd = start.X < end.X ? end.X : start.X;
            return Enumerable.Range(rangeStart, rangeEnd - rangeStart + 1)
               .Select(val => new Point(val, start.Y))
               .ToArray();
         }
         // diagonal 45’
         if (useDiagonal)
         {
            var xIncrement = start.X < end.X ? 1 : -1;
            var yIncrement = start.Y < end.Y ? 1 : -1;
            var points = new List<Point> { start };
            var curPoint = start;
            while (curPoint != end)
            {
               curPoint = new Point(curPoint.X + xIncrement, curPoint.Y + yIncrement);
               points.Add(curPoint);
            }
            return points.ToArray();
         }
         
         return Array.Empty<Point>();
      }

      private (Point start, Point end) ParseLine(string line)
      {
         var pointsText = line.Split("->");
         if (pointsText.Length == 0) throw new Exception("Error parsing line: "+ line);
         var startCoordinates = pointsText[0].Trim()
            .Split(',')
            .Select(int.Parse)
            .ToArray();
         var endCoordinates = pointsText[1].Trim()
            .Split(',')
            .Select(int.Parse)
            .ToArray();
         return (new Point(startCoordinates[0], startCoordinates[1]), new Point(endCoordinates[0], endCoordinates[1]));
      }

   }
}
