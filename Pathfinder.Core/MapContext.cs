using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Core
{
    public class MapContext
    {
        public int Width { get; }
        public int Height { get; }

        public MapContext(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public List<Point> Obstacles { get; } = new List<Point>();
        public Point StartingPoint { get; set; }
        public Point TargetPoint { get; set; }

        /// <summary>
        /// Checks if a point is within the bounds of the map
        /// </summary>
        /// <param name="testPoint">The point to test</param>
        /// <returns></returns>
        public bool IsValid(Point testPoint)
        {
            return testPoint.X >= 0
                   && testPoint.X < Width
                   && testPoint.Y >= 0
                   && testPoint.Y < Height;
        }

        /// <summary>
        /// Checks if a point contains an obstacle
        /// </summary>
        /// <param name="testPoint"></param>
        /// <returns></returns>
        public bool IsObstacle(Point testPoint) => Obstacles.Contains(testPoint);

        /// <summary>
        /// Adds an obstacle
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">Thy y coordinate</param>
        public void AddObstacle(int x, int y) => Obstacles.Add(new Point(x, y));
    }
}