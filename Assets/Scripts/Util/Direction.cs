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
    }
}