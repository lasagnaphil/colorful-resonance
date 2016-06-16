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

    // protected override void Start()
    // {
    //     sr = GetComponent<SpriteRenderer>();
    // }

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();
        if (destroyed) return sequence;

        Player player = GameStateManager.Instance.player;
        Position PlayerPos = player.GetComponent<Position>();

        deltaX = PlayerPos.X - pos.X - 1;
        deltaY = PlayerPos.Y - pos.Y - 1;

        if (moveCancelled)
        {
            moveCancelled = false;
            return sequence;
        }
        else
        {
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
                            if (CheckPosition(pos.X + 1, pos.Y))
                            {
                                AnimatedMove(sequence, pos.X + 1, pos.Y);
                            }
                        }
                        else
                        {
                            if (CheckPosition(pos.X + 3, pos.Y))
                            {
                                AnimatedMove(sequence, pos.X + 3, pos.Y);
                            }
                        }
                    }
                    else if (deltaX < 0)
                    {
                        if (deltaX > -3)
                        {
                            if (CheckPosition(pos.X - 1, pos.Y))
                            {
                                AnimatedMove(sequence, pos.X - 1, pos.Y);
                            }
                        }
                        else
                        {
                            if (CheckPosition(pos.X - 3, pos.Y))
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
                            if (CheckPosition(pos.X, pos.Y + 1))
                            {
                                AnimatedMove(sequence, pos.X, pos.Y + 1);
                            }
                        }
                        else
                        {
                            if (CheckPosition(pos.X, pos.Y + 3))
                            {
                                AnimatedMove(sequence, pos.X, pos.Y + 3);
                            }
                        }
                    }
                    else if (deltaY < 0)
                    {
                        if (deltaY > -3)
                        {
                            if (CheckPosition(pos.X, pos.Y - 1))
                            {
                                AnimatedMove(sequence, pos.X, pos.Y - 1);
                            }
                        }
                        else
                        {
                            if (CheckPosition(pos.X, pos.Y - 3))
                            {
                                AnimatedMove(sequence, pos.X, pos.Y - 3);
                            }
                        }
                    }
                }
                CoolTime = 0;
            }
        }

        return sequence;
    }

    bool CheckPosition(int a, int b)
    {
        if (TileManager.Instance.GetTileType(a, b) != TileType.Wall && TileManager.Instance.GetTileType(a, b) != TileType.None)
            return true;
        else return false;
    }
}

