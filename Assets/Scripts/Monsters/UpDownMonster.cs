namespace Monsters
{
    public class UpDownMonster : Monster
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