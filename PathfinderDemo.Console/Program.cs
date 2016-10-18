using System;
using System.Collections.Generic;
using System.Linq;

using Pathfinder.Core;

namespace PathfinderDemo.ConsoleUI
{
    public static class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                var map = new MapContext(24, 12);

                //SetUpSimpleTest(map);
                SetUpRandomTest(map);

                var searcher = new AStarPathFinder(diagonalMoves: false);
                var path = searcher.FindPath(map);

                DrawMap(map, path);

                key = Console.ReadKey(intercept: true);
            } while (key.Key != ConsoleKey.Q);
        }

        static void SetUpRandomTest(MapContext map)
        {
            map.StartingPoint = RandomPoint(map);
            do
            {
                map.TargetPoint = RandomPoint(map);
            } while (map.TargetPoint == map.StartingPoint);

            for (int i = 0; i < 50; i++)
            {
                var point = RandomPoint(map);
                while (point == map.StartingPoint || point == map.TargetPoint || map.IsObstacle(point))
                    point = RandomPoint(map);

                map.Obstacles.Add(point);
            }
        }

        private static readonly Random _rng = new Random();
        static Point RandomPoint(MapContext map)
        {
            return new Point(_rng.Next(map.Width), _rng.Next(map.Height));
        }

        // ReSharper disable once UnusedMember.Local
        static void SetUpSimpleTest(MapContext map)
        {
            map.StartingPoint = new Point(0, 0);
            map.TargetPoint = new Point(9, 9);

            map.AddObstacle(0, 3);
            map.AddObstacle(1, 3);
            map.AddObstacle(2, 3);

            map.AddObstacle(4, 3);
            map.AddObstacle(5, 3);
            map.AddObstacle(6, 3);
            map.AddObstacle(7, 3);
            map.AddObstacle(8, 3);
            map.AddObstacle(9, 3);

            map.AddObstacle(3, 1);
        }

        private static void DrawMap(MapContext map, Path path)
        {
            var steps = path?.Steps ?? new Point[0];

            WriteHorizontalBorder(map, bottom: false);
            for (var y = 0; y < map.Height; y++)
            {
                Console.Write("│");
                for (var x = 0; x < map.Width; x++)
                {
                    var p = new Point(x, y);
                    if (p == map.StartingPoint)
                        WriteColor("0", ConsoleColor.DarkYellow);
                    else if (p == map.TargetPoint)
                        WriteColor("$", ConsoleColor.Green);
                    else if (steps.Contains(p))
                        WriteColor("·", ConsoleColor.Blue);
                    else if (map.Obstacles.Contains(p))
                        WriteColor("█", ConsoleColor.DarkGray);
                    else
                        Console.Write(" ");
                }

                Console.Write("│");
                Console.WriteLine();
            }
            WriteHorizontalBorder(map, bottom: true);
        }

        private static void WriteColor(string s, ConsoleColor color)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(s);
            Console.ForegroundColor = old;
        }

        private static void WriteHorizontalBorder(MapContext map, bool bottom)
        {
            Console.Write(bottom ? "└" : "┌");
            for(int i = 0; i < map.Width; i++)
                Console.Write("─");
            Console.Write(bottom ? "┘" : "┐");
            Console.WriteLine();
        }
    }
}
