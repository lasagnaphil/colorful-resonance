namespace Monsters
{
    public class BasicMonster : Monster
    {
        public bool isFacingUp = false;

        public TileType immuneColor;

        protected override void OnTurn()
        {
            base.OnTurn();
            pos.Y = isFacingUp ? pos.Y + 1 : pos.Y - 1;
            isFacingUp = !isFacingUp;

            TileType currentTileType = TileManager.Instance.GetTileType(pos.X, pos.Y);
            if (currentTileType != immuneColor && currentTileType != TileType.None)
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