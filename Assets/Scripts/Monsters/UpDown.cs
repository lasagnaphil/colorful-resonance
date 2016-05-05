namespace Monsters
{
    public class UpDown : Monster
    {
        public bool isFacingUp = false;

        protected override void OnTurn()
        {
            base.OnTurn();
            if (isFacingUp) Move(pos.X, pos.Y + 1);
            else Move(pos.X, pos.Y - 1);
            isFacingUp = !isFacingUp;
        }
    }
}