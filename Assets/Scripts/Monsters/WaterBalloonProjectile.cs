using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class WaterBalloonProjectile : Projectile
{
    public TileColor BombColor;

    private int turn = 0;

    int deltaX;
    int deltaY;

    protected virtual void Start()
    {
        base.Start();
        updateDirection = false;
    }

    protected override Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();
        base.OnTurn();
        deltaX = player.pos.X - pos.X;
        deltaY = player.pos.Y - pos.Y;

        if (GameStateManager.Instance.CheckOrbPosition(pos.X, pos.Y) == null)
            TileManager.Instance.SetTileColor(pos.X, pos.Y, BombColor);

        if (Mathf.Abs(deltaX) == 0 && Mathf.Abs(deltaY) == 0)
        {
            player.ApplyDamage(Damage);
        }

        Destroy(gameObject);
        return CheckAndDestroy(sequence);
    }
}
