using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Core
{
    internal class AStarNode
    {
        /// <summary>
        /// F is the weight for the current node
        /// </summary>
        public int F => G + H;

        /// <summary>
        /// G is the cost to get to the current node from the starting point
        /// </summary>
        public int G { get; set; }

        /// <summary>
        /// H is a heuristic guess of how far it is to get to the target from the current
        /// node
        /// </summary>
        public int H { get; set; }

        public Point Point { get; set; }

        /// <summary>
        /// Using Parent, we can walk backwards from the target to enumerate the path
        /// </summary>
        public AStarNode Parent { get; set; }

        public IEnumerable<Point> TraceToStart()
        {
            var node = this;
            while (node.Parent != null)
            {
                yield return node.Point;
                node = node.Parent;
            }

        }
    }
}
