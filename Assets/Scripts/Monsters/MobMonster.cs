using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class MobMonster : Monster
{
    int Cleaning;
    int CoolTime;

    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();

        if (TileManager.Instance.GetTileColor(pos.X, pos.Y) == TileColor.Blue)
            Cleaning++;
        else
        {
            Cleaning = 0;

            if (CoolTime < 1)
                CoolTime++;
            else if (CoolTime == 1)
            {
                if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
                {
                    if (delta.x > 0)
                    {
                        if (CheckTileIsNormal(pos.X + 1, pos.Y))
                        {
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                        }
                    }
                    else if (delta.x < 0)
                    {
                        if (CheckTileIsNormal(pos.X - 1, pos.Y))
                        {
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                        }
                    }
                }
                else
                {
                    if (delta.y > 0)
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y + 1))
                        {
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                        }
                    }
                    else if (delta.y < 0)
                    {
                        if (CheckTileIsNormal(pos.X, pos.Y - 1))
                        {
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
                        }
                    }
                }
                CoolTime = 0;
            }
            else
            {
                if (delta.y > 0)
                {
                    if (CheckTileIsNormal(pos.X, pos.Y + 1))
                    {
                        AnimatedMove(sequence, pos.X, pos.Y + 1);
                    }
                }
                else if (delta.y < 0)
                {
                    if (CheckTileIsNormal(pos.X, pos.Y - 1))
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
    }

    protected override void WhenDestroyed()
    {
        base.WhenDestroyed();
    }
}

