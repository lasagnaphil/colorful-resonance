﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class VShooterMonster : Monster
{
    public bool isFacingUp = false;

    public Projectile projectilePrefab;

    private int shootTimer = 0;
    public int ShootInterval;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();

        shootTimer++;

        if (shootTimer == ShootInterval)
        {
            Direction moveDir = isFacingUp ? Direction.Up : Direction.Down;
            GameStateManager.Instance.SpawnProjectile(projectilePrefab, pos.X, pos.Y, moveDir);
            isFacingUp = !isFacingUp;
            shootTimer = 0;
        }

        if (isFacingUp)
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
