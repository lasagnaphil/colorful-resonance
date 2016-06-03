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
            {
                MovingDirection = Direction.Right;
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (deltaX < 0)
            {
                MovingDirection = Direction.Left;
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (deltaY > 0)
            {
                MovingDirection = Direction.Up;
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
            }
            else if (deltaY < 0)
            {
                MovingDirection = Direction.Down;
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
            }
        }

        return sequence;
    }
}
