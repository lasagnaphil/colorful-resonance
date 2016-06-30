using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public enum Direction
    {
        None,
        Left,
        Right,
        Down,
        Up
    }

    public static class DirectionHelper
    {
        public static Vector2i ToVector2i(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left: return new Vector2i(-1, 0);
                case Direction.Right: return new Vector2i(1, 0);
                case Direction.Down: return new Vector2i(0, -1);
                case Direction.Up: return new Vector2i(0, 1);
                default: return new Vector2i(0, 0);
            }
        }

        public static Quaternion ToQuaternion(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return Quaternion.Euler(0, 0, 0);
                case Direction.Right:
                    return Quaternion.Euler(0, 180, 0);
                case Direction.Down:
                    return Quaternion.Euler(0, 0, 90);
                case Direction.Up:
                    return Quaternion.Euler(0, 0, 270);
                default:
                    return Quaternion.Euler(0, 0, 0);
            }
        }

        public static List<Direction> ToDirections(Vector2i vec)
        {
            var dirs = new List<Direction>();

            if (vec.x > 0) dirs.Add(Direction.Right);
            else if (vec.x < 0) dirs.Add(Direction.Left);
            else if (vec.y > 0) dirs.Add(Direction.Up);
            else if (vec.y < 0) dirs.Add(Direction.Down);
            else dirs.Add(Direction.None);

            return dirs;
        }

        public static List<Direction> ToDirections(int x, int y)
        {
            return ToDirections(new Vector2i(x, y));
        }

        public static Direction ToDirectionX(Vector2i vec)
        {
            if (vec.x > 0) return Direction.Right;
            if (vec.x < 0) return Direction.Left;
            if (vec.y > 0) return Direction.Up;
            if (vec.y < 0) return Direction.Down;
            return Direction.None;
        }

        public static Direction ToDirectionX(int x, int y)
        {
            return ToDirectionX(x, y);
        }

        public static Direction ToDirectionY(Vector2i vec)
        {
            if (vec.y > 0) return Direction.Up;
            if (vec.y < 0) return Direction.Down;
            if (vec.x > 0) return Direction.Right;
            if (vec.x < 0) return Direction.Left;
            return Direction.None;
        }

        public static Direction ToDirectionY(int x, int y)
        {
            return ToDirectionY(new Vector2i(x, y));
        }
    }
}