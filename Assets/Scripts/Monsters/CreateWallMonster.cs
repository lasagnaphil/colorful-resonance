using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class CreateWallMonster : Monster
{
    public Projectile MakeWallProjectile;

    int SpawnPointX;
    int SpawnPointY;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();
        
        if (destroyed)
        {
            SpawnPointX = GameStateManager.Instance.player.GetComponent<Position>().X;
            SpawnPointY = GameStateManager.Instance.player.GetComponent<Position>().Y;

            Direction moveDir = Direction.None;
            GameStateManager.Instance.SpawnProjectile(MakeWallProjectile, SpawnPointX, SpawnPointY, moveDir);
        }

        return sequence;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (CheckBeforeDestroy)
        {
            TileManager.Instance.SetTileType(pos.X, pos.Y, TileType.Wall);
            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
        }
    }
}