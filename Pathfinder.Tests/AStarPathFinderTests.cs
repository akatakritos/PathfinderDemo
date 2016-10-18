using System;
using System.Collections.Generic;
using System.Linq;

using NFluent;

using Pathfinder.Core;

using Xunit;

namespace Pathfinder.Tests
{
    public class AStarPathFinderTests
    {
        [Fact]
        public void FindsStraightPath()
        {
            var map = new MapContext(5, 3);
            map.StartingPoint = new Point(0, 1);
            map.TargetPoint = new Point(4, 1);

            var subject = new AStarPathFinder();
            var path = subject.FindPath(map);

            Check.That(path.Steps).ContainsExactly(
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1),
                new Point(4, 1));
        }

        [Fact]
        public void GoesAroundObstacles()
        {
            var map = new MapContext(5, 3);
            map.StartingPoint = new Point(0, 1);
            map.TargetPoint = new Point(4, 1);
            map.Obstacles.Add(new Point(2,2));
            map.Obstacles.Add(new Point(2,1));

            var subject = new AStarPathFinder();
            var path = subject.FindPath(map);

            Check.That(path.Steps).Not.Contains(new Point(2, 1));
        }

        [Fact]
        public void NoPathReturnsNull()
        {
            var map = new MapContext(5, 3);
            map.StartingPoint = new Point(0, 0);
            map.TargetPoint = new Point(4, 1);
            map.AddObstacle(0, 1);
            map.AddObstacle(1, 0);
            map.AddObstacle(1, 1);

            var subject = new AStarPathFinder();
            var path = subject.FindPath(map);

            Check.That(path).IsNull();
        }
    }
}
