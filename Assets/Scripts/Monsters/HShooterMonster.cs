using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class HShooterMonster: Monster
{
    public bool isFacingRight = false;
    
    public Projectile projectilePrefab;

    private int shootTimer = 0;
    public int ShootInterval;

    protected override void OnTurn(Sequence sequence)
    {
        shootTimer++;

        if (shootTimer == ShootInterval)
        {
            Direction moveDir = isFacingRight ? Direction.Right : Direction.Left;
            SpawnProjectile(projectilePrefab, pos.X, pos.Y, moveDir);
            soundManager.Play(SoundManager.Sounds.Shoot);
            isFacingRight = !isFacingRight;
            shootTimer = 0;
        }

        if (isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    protected override void WhenDestroyed()
    {
        base.WhenDestroyed();
    }
}
