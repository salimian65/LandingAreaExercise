using System;
using System.Collections.Generic;

namespace LandingPlatform.Domain.Platforms
{
    public class Position
    {
        private readonly int x;

        private readonly int y;
            
        private readonly HashSet<string> perimeter = new();

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
            SetCoordinatePerimeter();
        }

        public static bool operator <(Position a, Position b)
        {
            if (a == null || b == null) return false;

            return a.x < b.x && a.y < b.y;
        }

        public static bool operator >(Position a, Position b)
        {
            if (a == null || b == null) return false;

            return a.x > b.x && a.y > b.y;
        }

        public static bool operator <=(Position a, Position b)
        {
            if (a == null || b == null) return false;

            return a.x <= b.x && a.y <= b.y;
        }

        public static bool operator >=(Position a, Position b)
        {
            if (a == null || b == null) return false;

            return a.x >= b.x && a.y >= b.y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Position coordinate) return false;

            return x == coordinate.x && y == coordinate.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public override string ToString()
        {
            return GetString(x, y);
        }

        internal bool IsItOnPerimeter(Position position)
        {
            return perimeter.Contains(position.ToString());
        }

        private static string GetString(int x, int y)
        {
            return $"{x},{y}";
        }

        private void SetCoordinatePerimeter()
        {
            for (var x = this.x - 1; x <= this.x + 1; x++)
            {
                for (var y = this.y - 1; y <= this.y + 1; y++)
                {
                    if (x == this.x && y == this.y) continue;

                    perimeter.Add(GetString(x, y));
                }
            }
        }
    }
}