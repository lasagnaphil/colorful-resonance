using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class PaintWallMonster : Monster
{
    public Projectile PaintBomb;

    Direction moveDir;
    int deltaX;
    int deltaY;

    protected override void WhenDestroyed()
    {
        SpawnProjectile(PaintBomb, pos.X, pos.Y, DirectionFromPlayer());
    }
}
