using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class ShieldMonster : Monster
{
    public Sprite ShieldOff;

    int CoolTime;

    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();

        if (Health == 1)
            GetComponent<SpriteRenderer>().sprite = ShieldOff;

        if (CoolTime < 2)
            CoolTime++;
        else if(CoolTime == 2)
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
    }

    protected override void WhenDestroyed()
    {
        base.WhenDestroyed();
    }
}

