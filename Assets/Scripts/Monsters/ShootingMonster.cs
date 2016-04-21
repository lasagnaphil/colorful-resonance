﻿using UnityEngine;
using System.Collections;
using Utils;

public class ShootingMonster : Monster
{
    public bool isFacingRight = false;

    public Projectile projectilePrefab;

    private int shootTimer = 0;
    public int ShootInterval { get; set; }

    protected override void OnTurn()
    {
        base.OnTurn();
        shootTimer++;
        if (shootTimer == ShootInterval)
        {
            Direction moveDir = isFacingRight ? Direction.Right : Direction.Left;
            GameStateManager.Instance.SpawnProjectile(projectilePrefab, pos.X, pos.Y, moveDir);
            isFacingRight = !isFacingRight;
            shootTimer = 0;
        }
    }
}
