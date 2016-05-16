using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class BoomBoomMonster : Monster
{
    public Projectile BombProjectile;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();

        return sequence;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (!GameStateManager.Instance.IsLoading)
        {
            Direction moveDir = Direction.None;
            GameStateManager.Instance.SpawnProjectile(BombProjectile, pos.X, pos.Y, moveDir);
        }
    }
}