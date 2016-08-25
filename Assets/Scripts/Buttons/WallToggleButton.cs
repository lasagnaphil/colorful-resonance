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
            }
            else
            {
                TileManager.Instance.SetTileType(
                    wallTogglePos.x, wallTogglePos.y,
                    isWallOnButtonOff ? TileType.Wall : TileType.Normal);
            }
            if (TileManager.Instance.GetTileType(wallTogglePos.x, wallTogglePos.y) == TileType.Wall)
            {
                Monster monster = GameStateManager.Instance.CheckMonsterPosition(wallTogglePos.x, wallTogglePos.y);
                if (monster != null)
                {
                    monster.Health -= 100;
                    monster.CheckHealth();
                }
                Player player = GameStateManager.Instance.player;
                if (player.pos.X == wallTogglePos.x && player.pos.Y == wallTogglePos.y)
                {
                    player.ApplyDamage(100);
                }
            }
        }
    }
}