namespace Buttons
{
    public class WallToggleButton : Button
    {
        public Vector2i wallTogglePos;
        public bool isWallOnButtonStateOff;
        public TileColor wallColor;

        protected virtual void OnTurn()
        {
            base.OnTurn();
            Tile wallTile = TileManager.Instance.GetTile(wallTogglePos.x, wallTogglePos.y);

            if (IsActive)
            {
                wallTile.Data.type = isWallOnButtonStateOff ? TileType.Normal : TileType.Wall;
            }
            else
            {
                wallTile.Data.type = isWallOnButtonStateOff ? TileType.Wall : TileType.Normal;
            }
        }
    }
}