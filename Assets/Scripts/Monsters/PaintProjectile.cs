﻿using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class PaintProjectile : Projectile
{
    public TileColor BombColor;

    protected override Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();
        base.OnTurn();

        if ((TileManager.Instance.GetTileType(pos.X, pos.Y) == TileType.Wall || TileManager.Instance.GetTileType(pos.X, pos.Y) == TileType.None) && Type != ProjectileType.GoThrough)
        {
            Destroy(gameObject);
            for(int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (TileManager.Instance.GetTileType(pos.X - 1 + k, pos.Y - 1 + j) != TileType.None &&
                        GameStateManager.Instance.CheckOrbPosition(pos.X - 1 + k, pos.Y - 1 + j) == null)
                    {
                        TileManager.Instance.SetTileColor(pos.X - 1 + k, pos.Y - 1 + j, BombColor);
                    }
                }
            }
        }

        return CheckAndDestroy(sequence);
    }
}
