using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class HomingMissileMonster : Monster
{
   public Projectile HomingProjectile;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();

        return sequence;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (GameStateManager.Instance.isLoading)
        {
            Direction moveDir = Direction.Left;
            GameStateManager.Instance.SpawnProjectile(HomingProjectile, pos.X, pos.Y, moveDir);
        }
    }
}

