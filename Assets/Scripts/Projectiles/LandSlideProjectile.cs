using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class LandSlideProjectile : Projectile
{
    protected override void Start()
    {
        base.Start();
        updateDirection = false;
    }

    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();
        if (Mathf.Abs(delta.x) == 0 && Mathf.Abs(delta.y) == 0)
        {
            player.Health = 0;
        }
        GameStateManager.Instance.soundManager.Play(SoundManager.Sounds.Explosion);
        TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.None);
        TileManager.Instance.SetTileType(pos.X, pos.Y, TileType.None);

        Destroy(gameObject);
    }
}
