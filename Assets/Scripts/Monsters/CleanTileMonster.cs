using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class CleanTileMonster : Monster
{
    int CoolTime;

    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();

        if (CoolTime < 2)
            CoolTime++;
        else if (CoolTime == 2)
        {
            if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
            {
                if (delta.x > 0)
                {
                    if (CheckTileIsNormal(pos.X + 1, pos.Y))
                    {
                        AnimatedMove(sequence, pos.X + 1, pos.Y);
                        if (GameStateManager.Instance.CheckOrbPosition(pos.X, pos.Y) == null)
                            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
                    }
                }
                else if (delta.x < 0)
                {
                    if (CheckTileIsNormal(pos.X - 1, pos.Y))
                    {
                        AnimatedMove(sequence, pos.X - 1, pos.Y);
                        if (GameStateManager.Instance.CheckOrbPosition(pos.X, pos.Y) == null)
                            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
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
                        if (GameStateManager.Instance.CheckOrbPosition(pos.X, pos.Y) == null)
                            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
                    }
                }
                else if (delta.y < 0)
                {
                    if (CheckTileIsNormal(pos.X, pos.Y - 1))
                    {
                        AnimatedMove(sequence, pos.X, pos.Y - 1);
                        if (GameStateManager.Instance.CheckOrbPosition(pos.X, pos.Y) == null)
                            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
                    }
                }
            }
            CoolTime = 0;
        }
    }
}

