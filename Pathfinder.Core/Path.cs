using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Core
{
    public class Path
    {
        public Path(IReadOnlyList<Point> steps)
        {
            Steps = steps;
        }

        public IReadOnlyList<Point> Steps { get; }
    }
}