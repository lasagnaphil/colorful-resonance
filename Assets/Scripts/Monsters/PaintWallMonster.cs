using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class PaintWallMonster : Monster
{
    public Projectile PaintBomb;

    Direction moveDir;
    int deltaX;
    int deltaY;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();

        return sequence;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        if (CheckBeforeDestroy)
        {
            GameObject player = GameObject.Find("Player");
            

            deltaX = player.GetComponent<Position>().X - pos.X;
            deltaY = player.GetComponent<Position>().Y - pos.Y;

            if (deltaX > 0)
                moveDir = Direction.Right;
            else if (deltaX < 0)
                moveDir = Direction.Left;
            else if (deltaY > 0)
                moveDir = Direction.Up;
            else if (deltaY < 0)
                moveDir = Direction.Down;

            GameStateManager.Instance.SpawnProjectile(PaintBomb, pos.X, pos.Y, moveDir);
        }
    }
}
