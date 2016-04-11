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

            if (TileManager.Instance.GetTileType(pos.X, pos.Y) == TileType.Yellow)
            {
                Damage -= 1;
            }
            if (Damage <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}