namespace Monsters
{
    public class BasicMonster : Monster
    {
        public bool isFacingUp = false;

        protected override void OnTurn()
        {
            base.OnTurn();
            pos.Y = isFacingUp ? pos.Y + 1 : pos.Y - 1;
            isFacingUp = !isFacingUp;
        }
    }
}