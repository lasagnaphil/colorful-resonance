using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class CreateWallMonster : Monster
{
    public Projectile MakeWallProjectile;

    int SpawnPointX;
    int SpawnPointY;

    protected override void OnTurn(Sequence sequence)
    {
        if (destroyed)
        {
            SpawnPointX = GameStateManager.Instance.player.GetComponent<Position>().X;
            SpawnPointY = GameStateManager.Instance.player.GetComponent<Position>().Y;

            Direction moveDir = Direction.None;
            SpawnProjectile(MakeWallProjectile, SpawnPointX, SpawnPointY, moveDir);
        }
    }

    protected override void WhenDestroyed()
    {
        TileManager.Instance.SetTileType(pos.X, pos.Y, TileType.Wall);
        TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
    }
}