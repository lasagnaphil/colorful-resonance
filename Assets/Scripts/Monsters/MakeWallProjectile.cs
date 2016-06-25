using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class MakeWallProjectile : Projectile
{
    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();
        if (Mathf.Abs(delta.x) == 0 && Mathf.Abs(delta.y) == 0)
        {
            player.Health = 0;
        }

        TileManager.Instance.SetTileType(pos.X, pos.Y, TileType.Wall);
        TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);

        Destroy(gameObject);
    }
}
