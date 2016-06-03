﻿using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class MakeWallProjectile : Projectile
{
    int deltaX;
    int deltaY;

    protected override Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();
        base.OnTurn();
        deltaX = player.pos.X - pos.X;
        deltaY = player.pos.Y - pos.Y;

        if (Mathf.Abs(deltaX) == 0 && Mathf.Abs(deltaY) == 0)
        {
            player.Health = 0;
        }

        TileManager.Instance.SetTileType(pos.X, pos.Y, TileType.Wall);
        TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);

        Destroy(gameObject);
        return CheckAndDestroy(sequence);
    }
}
