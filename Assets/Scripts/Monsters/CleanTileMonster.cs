using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class CleanTileMonster : Monster
{
    int deltaX;
    int deltaY;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();
        Player player = GameStateManager.Instance.player;
        Position PlayerPos = player.GetComponent<Position>();

        deltaX = PlayerPos.X - pos.X;
        deltaY = PlayerPos.Y - pos.Y;

        if (moveCancelled)
        {
            moveCancelled = false;
            return sequence;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            deltaX = deltaX + 1;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            deltaX = deltaX - 1;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            deltaY = deltaY - 1;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            deltaY = deltaY + 1;

        if (deltaX > 0)
            AnimatedMove(sequence, pos.X + 1, pos.Y);
        else if (deltaX < 0)
            AnimatedMove(sequence, pos.X - 1, pos.Y);
        else if (deltaY > 0)
            AnimatedMove(sequence, pos.X, pos.Y + 1);
        else if (deltaY < 0)
            AnimatedMove(sequence, pos.X, pos.Y - 1);

        return sequence;
    }
}

