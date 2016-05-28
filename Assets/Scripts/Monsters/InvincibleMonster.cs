using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

namespace Monsters
{
    public class InvincibleMonster : Monster
    {
        int deltaX;
        int deltaY;
        int CoolTime;

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

            if (CoolTime < 6)
                CoolTime++;
            else CoolTime = 1;

            if (CoolTime > 3)
            {
                monstersColor = TileColor.Black;

                if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
                {
                    if (deltaX > 0)
                    {
                        if (CheckPosition(pos.X + 1, pos.Y))
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                    }
                    else if (deltaX < 0)
                    {
                        if (CheckPosition(pos.X - 1, pos.Y))
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                    }
                }
                else
                {
                    if (deltaY > 0)
                    {
                        if (CheckPosition(pos.X, pos.Y + 1))
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                    }
                    else if (deltaY < 0)
                    {
                        if (CheckPosition(pos.X, pos.Y - 1))
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
                    }
                }
            }
            else
            {
                monstersColor = TileColor.Yellow;

                if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
                {
                    if (deltaX > 0)
                    {
                        if(CheckPosition(pos.X - 1, pos.Y))
                            AnimatedMove(sequence, pos.X - 1, pos.Y);
                    }
                    else if (deltaX < 0)
                    {
                        if (CheckPosition(pos.X + 1, pos.Y))
                            AnimatedMove(sequence, pos.X + 1, pos.Y);
                    }
                }
                else
                {
                    if (deltaY > 0)
                    {
                        if(CheckPosition(pos.X, pos.Y - 1))
                            AnimatedMove(sequence, pos.X, pos.Y - 1);
                    }
                    else if (deltaY < 0)
                    {
                        if (CheckPosition(pos.X, pos.Y + 1))
                            AnimatedMove(sequence, pos.X, pos.Y + 1);
                    }
                }
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
}