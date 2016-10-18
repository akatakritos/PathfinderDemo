using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Core
{
    public struct Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region R# Generated

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Point && Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }

        #endregion

        public override string ToString() => $"({X}, {Y})";

        public Point Translate(int deltaX, int deltaY)
        {
            return new Point(X + deltaX, Y + deltaY);
        }

    }
}