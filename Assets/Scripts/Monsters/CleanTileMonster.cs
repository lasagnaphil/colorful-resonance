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

        if (deltaX > 0)
        {
            AnimatedMove(sequence, pos.X + 1, pos.Y);
            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
        }
        else if (deltaX < 0)
        {
            AnimatedMove(sequence, pos.X - 1, pos.Y);
            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
        }
        else if (deltaY > 0)
        {
            AnimatedMove(sequence, pos.X, pos.Y + 1);
            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
        }
        else if (deltaY < 0)
        {
            AnimatedMove(sequence, pos.X, pos.Y - 1);
            TileManager.Instance.SetTileColor(pos.X, pos.Y, TileColor.White);
        }

        return sequence;
    }
}

