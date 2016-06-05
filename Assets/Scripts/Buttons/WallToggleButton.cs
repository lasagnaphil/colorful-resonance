using UnityEngine;

namespace Buttons
{
    public class WallToggleButton : Button
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
                Debug.Log("isActive true");
            }
            else
            {
                TileManager.Instance.SetTileType(
                    wallTogglePos.x, wallTogglePos.y,
                    isWallOnButtonOff ? TileType.Wall : TileType.Normal);
                Debug.Log("isActive false");
            }
        }
    }
}