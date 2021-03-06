﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class BlinkMonster : Monster
{
    int deltaX;
    int deltaY;
    int CoolTime;
    bool Danger = false;

    SpriteRenderer sr;
    public Sprite[] images;

    protected override void OnTurn(Sequence sequence)
    {
        deltaX = DiffFromPlayer().x;
        deltaY = DiffFromPlayer().y;

        sr = gameObject.GetComponent<SpriteRenderer>();
        if (CoolTime == 0)
        {
            sr.sprite = images[1];
            CoolTime++;
        }
        else if (CoolTime == 1)
        {
            sr.sprite = images[2];
            CoolTime++;
        }
        else if (CoolTime == 2)
        {
            sr.sprite = images[0];
            if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
            {
                if (deltaX > 0)
                {
                    if (deltaX < 3)
                    {
                        if (CheckTileIsNormal(pos.X + 1, pos.Y))
                        {
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                        }
                    }
                    else
                    {
                        if (CheckTileIsNormal(pos.X + 3, pos.Y))
                        {
                            AnimatedMove(sequence, pos.X + 3, pos.Y);
                        }
                    }
                }
                else if (deltaX < 0)
                {
                    if (deltaX > -3)
                    {
                        if (CheckTileIsNormal(pos.X - 1, pos.Y))
                        {
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                        }
                    }
                    else
                    {
                        if (CheckTileIsNormal(pos.X - 3, pos.Y))
                        {
                            AnimatedMove(sequence, pos.X - 3, pos.Y);
                        }
                    }
                }
            }
            else
            {
                if (deltaY > 0)
                {
                    if (deltaY < 3)
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y + 1))
                        {
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                        }
                    }
                    else
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y + 3))
                        {
                            AnimatedMove(sequence, pos.X, pos.Y + 3);
                        }
                    }
                }
                else if (deltaY < 0)
                {
                    if (deltaY > -3)
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y - 1))
                        {
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
                        }
                    }
                    else
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y - 3))
                        {
                            AnimatedMove(sequence, pos.X, pos.Y - 3);
                        }
                    }
                }
            }
            CoolTime = 0;
        }
    }

    protected override void WhenDestroyed()
    {
        base.WhenDestroyed();
    }
}

