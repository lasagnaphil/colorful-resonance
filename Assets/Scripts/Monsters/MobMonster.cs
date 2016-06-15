using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class MobMonster : Monster
{
    int deltaX;
    int deltaY;
    int Cleaning;
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

        if (TileManager.Instance.GetTileColor(pos.X, pos.Y) == TileColor.Blue)
            Cleaning++;
        else
        {
            Cleaning = 0;

            if (CoolTime < 1)
                CoolTime++;
            else if (CoolTime == 1)
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
        }

        if (Cleaning == 2)
        {
            if (GameStateManager.Instance.CheckOrbPosition(pos.X, pos.Y) == null)
                TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
            Cleaning = 0;
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

