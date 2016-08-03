using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class HomingMissileMonster : Monster
{
    public Projectile HomingProjectile;

    protected override void WhenDestroyed()
    {
        base.WhenDestroyed();
        soundManager.Play(SoundManager.Sounds.Shoot);
        SpawnProjectile(HomingProjectile, pos.X, pos.Y, Direction.None);
    }
}

