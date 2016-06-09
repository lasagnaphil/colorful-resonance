using UnityEngine;

namespace Buttons
{
    // almost same functionality as WallToggleButton... 
    public class WallToggleLever : Lever
    {
        public Vector2i wallTogglePos;
        public bool isWallOnButtonOff;
        public TileColor wallColor;

        protected override void OnTurn()
        {
            base.OnTurn();
            if (IsActive)
            {
                TileManager.Instance.SetTileType(
                    wallTogglePos.x, wallTogglePos.y,
                    isWallOnButtonOff ? TileType.Normal : TileType.Wall);
            }
            else
            {
                TileManager.Instance.SetTileType(
                    wallTogglePos.x, wallTogglePos.y,
                    isWallOnButtonOff ? TileType.Wall : TileType.Normal);
            }
        }
    }
}