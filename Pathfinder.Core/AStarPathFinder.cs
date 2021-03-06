﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Core
{
    // https://www.raywenderlich.com/4946/introduction-to-a-pathfinding
    public class AStarPathFinder
    {
        public AStarPathFinder(bool diagonalMoves = false)
        {
            _possibleAdjacentDeltas = diagonalMoves ? MoveDeltas.Diagonal : MoveDeltas.Cardinal;
        }

        public Path FindPath(MapContext map)
        {
            // List of possible nodes to consider
            var openList = new List<AStarNode>();

            // List of nodes we've considered
            var closedList = new List<AStarNode>();

            // Create a node for the starting point
            var startNode = new AStarNode()
            {
                G = 0,
                H = CalculateHeuristic(map.StartingPoint, map.TargetPoint),
                Parent = null,
                Point = map.StartingPoint
            };

            openList.Add(startNode);

            while (openList.Any())
            {
                var shortestNode = openList.MinBy(n => n.F);

                // Move the node to the considered list
                closedList.Add(shortestNode);
                openList.Remove(shortestNode);

                // if the considered list contains the target,
                // we've found the shortest path
                var targetPointNode = closedList.FirstOrDefault(n => n.Point == map.TargetPoint);
                if (targetPointNode != null)
                {
                    return new Path(
                        targetPointNode.TraceToStart()
                            .Reverse()
                            .ToList());
                }

                var adjacent = Adjacent(map, shortestNode.Point);
                foreach (var testPoint in adjacent)
                {
                    // Skip this point if its already been considered
                    if (closedList.Any(n => n.Point == testPoint))
                        continue;

                    var existingNodeForPoint = openList.FirstOrDefault(n => n.Point == testPoint);
                    if (existingNodeForPoint == null)
                    {
                        // If this point is not in the list to consider, we'll
                        // add it so it can be considered later
                        openList.Add(new AStarNode()
                        {
                            Parent = shortestNode,
                            G = shortestNode.G + 1,
                            H = CalculateHeuristic(testPoint, map.TargetPoint),
                            Point = testPoint
                        });
                    }
                    else
                    {
                        // if this point is already in the list to consider,
                        // we can check to see if the current path to get to this
                        // node is any better. If its better, we'll replace it in the
                        // consider list
                        var potentiallyBetterNode = new AStarNode()
                        {
                            G = shortestNode.G + 1,
                            H = CalculateHeuristic(testPoint, map.TargetPoint),
                            Parent = shortestNode,
                            Point = testPoint
                        };


                        if (potentiallyBetterNode.F < existingNodeForPoint.F)
                        {
                            openList.Remove(existingNodeForPoint);
                            openList.Add(potentiallyBetterNode);
                        }
                    }
                }
            }

            // Couldn't find a path
            return null;
        }

        private static int CalculateHeuristic(Point p1, Point target)
        {
            // Manhattan distance
            //    (think city blocks: number of horizontal blocks + number of vertical blocks)
            return Math.Abs(target.X - p1.X) + Math.Abs(target.Y - p1.Y);
        }

        private IEnumerable<Point> Adjacent(MapContext map, Point current)
        {
            return _possibleAdjacentDeltas
                .Select(delta => current.Translate(delta.DeltaX, delta.DeltaY))
                .Where(testPoint => map.IsValid(testPoint) && !map.IsObstacle(testPoint));
        }

        private readonly IEnumerable<Delta> _possibleAdjacentDeltas;
    }

    internal struct Delta
    {
        public int DeltaX { get; }
        public int DeltaY { get; }

        public Delta(int deltaX, int deltaY)
        {
            DeltaX = deltaX;
            DeltaY = deltaY;
        }
    }

    internal static class MoveDeltas
    {
        public static readonly IEnumerable<Delta> Cardinal = new[]{
            new Delta(0, -1),
            new Delta(1, 0),
            new Delta(0, 1),
            new Delta(-1, 0),
        };

        public static readonly IEnumerable<Delta> Diagonal = new[]
        {
            new Delta(0, -1),
            new Delta(1, -1),
            new Delta(1, 0),
            new Delta(1, 1),
            new Delta(0, 1),
            new Delta(-1, 1),
            new Delta(-1, 0),
            new Delta(-1, -1)
        };
    }
}