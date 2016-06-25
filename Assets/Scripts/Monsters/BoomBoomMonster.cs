using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class BoomBoomMonster : Monster
{
    public Projectile BombProjectile;

    protected override void WhenDestroyed()
    {
        SpawnProjectile(BombProjectile, pos.X, pos.Y, Direction.None);
    }
}