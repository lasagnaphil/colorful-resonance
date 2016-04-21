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
    }
}