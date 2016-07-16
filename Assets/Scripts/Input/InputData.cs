namespace InputManagement
{
    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public class InputData
    {
        public bool CanBlink { get; set; }
        public Direction MoveDirection { get; set; }
    }
}