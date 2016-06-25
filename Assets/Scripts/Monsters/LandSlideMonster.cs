using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class LandSlideMonster : Monster
{
    public Projectile LandSlideProjectile;

    int CoolTime;

    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();

        if (CoolTime < 3)
            CoolTime++;
        else if(CoolTime == 3)
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
        Direction moveDir = Direction.None;
        SpawnProjectile(LandSlideProjectile, pos.X, pos.Y, moveDir);
    }
}

