﻿using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class Bomb : Projectile
{
    public TileColor BombColor;
    public int damage;

    private int turn = 0;

    int deltaX;
    int deltaY;

    protected override Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();
        base.OnTurn();
        deltaX = player.GetComponent<Position>().X - pos.X;
        deltaY = player.GetComponent<Position>().Y - pos.Y;

        TileManager.Instance.SetTileColor(pos.X, pos.Y, BombColor);
        TileManager.Instance.SetTileColor(pos.X + 1, pos.Y, BombColor);
        TileManager.Instance.SetTileColor(pos.X - 1, pos.Y, BombColor);
        TileManager.Instance.SetTileColor(pos.X, pos.Y + 1, BombColor);
        TileManager.Instance.SetTileColor(pos.X + 1, pos.Y + 1, BombColor);
        TileManager.Instance.SetTileColor(pos.X - 1, pos.Y + 1, BombColor);
        TileManager.Instance.SetTileColor(pos.X, pos.Y - 1, BombColor);
        TileManager.Instance.SetTileColor(pos.X + 1, pos.Y - 1, BombColor);
        TileManager.Instance.SetTileColor(pos.X - 1, pos.Y - 1, BombColor);

        if (Mathf.Abs(deltaX) <= 1 || Mathf.Abs(deltaY) <= 1)
        {
            player.ApplyDamage(damage);
        }

        Destroy(gameObject);
        return CheckAndDestroy(sequence);
    }
}
