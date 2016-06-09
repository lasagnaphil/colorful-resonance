using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class PunchBoxMonster : Monster
{
    public Sprite PunchBox;

    int deltaX;
    int deltaY;
    int state;
    int CoolTime;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();
        if (destroyed) return sequence;

        Player player = GameStateManager.Instance.player;
        Position PlayerPos = player.GetComponent<Position>();

        deltaX = PlayerPos.X - pos.X;
        deltaY = PlayerPos.Y - pos.Y;

        if (moveCancelled)
        {
            moveCancelled = false;
            return sequence;
        }

        if (state != 2 && Mathf.Abs(deltaX) <= 1 && Mathf.Abs(deltaY) <= 1)
        {
            state = 2;
            GetComponent<SpriteRenderer>().sprite = PunchBox;
            MaxHealth = 1;
            Health = 1;
        }

        if (state == 2)
        {
            if (CoolTime < 2)
                CoolTime++;
            else if (CoolTime == 2)
            {
                if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
                {
                    if (deltaX > 0)
                    {
                        if (CheckPosition(pos.X + 1, pos.Y))
                        {
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                        }
                    }
                    else if (deltaX < 0)
                    {
                        if (CheckPosition(pos.X - 1, pos.Y))
                        {
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                        }
                    }
                }
                else
                {
                    if (deltaY > 0)
                    {
                        if (CheckPosition(pos.X, pos.Y + 1))
                        {
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                        }
                    }
                    else if (deltaY < 0)
                    {
                        if (CheckPosition(pos.X, pos.Y - 1))
                        {
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
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

