using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class HomingMissileProjectile : Projectile
{
    int deltaX;
    int deltaY;

    protected override Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();
        base.OnTurn();
        deltaX = player.GetComponent<Position>().X - pos.X;
        deltaY = player.GetComponent<Position>().Y - pos.Y;

        if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
        {
            if (deltaX > 0)
                MovingDirection = Direction.Right;
            else if (deltaX < 0)
                MovingDirection = Direction.Left;
        }
        else
        {
            if (deltaY > 0)
                MovingDirection = Direction.Up;
            else if (deltaY < 0)
                MovingDirection = Direction.Down;
        }

        return sequence;
    }
}
