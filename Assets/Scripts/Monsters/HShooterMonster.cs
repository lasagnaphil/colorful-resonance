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

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();

        shootTimer++;

        if (shootTimer == ShootInterval)
        {
            Direction moveDir = isFacingRight ? Direction.Right : Direction.Left;
            GameStateManager.Instance.SpawnProjectile(projectilePrefab, pos.X, pos.Y, moveDir);
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

        return sequence;
    }
}
