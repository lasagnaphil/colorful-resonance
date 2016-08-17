using System;
using System.Collections;
using Utils;

namespace InputManagement
{
    public abstract class Command
    {
        public abstract void Execute(Player player);
    }

    public class MoveCommand : Command
    {
        public Direction dir;

        public MoveCommand(Direction dir)
        {
            this.dir = dir;
        }

        public override void Execute(Player player)
        {
            player.ArrowInactive();

            player.tempPos.x = player.pos.X; player.tempPos.y = player.pos.Y;

            bool nextTurn = false;

            switch (dir)
            {
                case Direction.Left:
	                player.TurnLeft();
	                player.tempPos.x--;
                    if (!(player.PositionCheck())) player.tempPos.x++;
                    else
                    {
                        player.soundmanager.Play(SoundManager.Sounds.Move1);
                        nextTurn = true;
                    }
                    break;
                case Direction.Right:
	                player.TurnRight();
                    player.tempPos.x++;
                    if (!(player.PositionCheck())) player.tempPos.x--;
                    else
                    {
                        player.soundmanager.Play(SoundManager.Sounds.Move1);
                        nextTurn = true;
                    }
                    break;
                case Direction.Up:
                    player.tempPos.y++;
                    if (!(player.PositionCheck())) player.tempPos.y--;
                    else
                    {
                        player.soundmanager.Play(SoundManager.Sounds.Move1);
                        nextTurn = true;
                    }
                    break;
                case Direction.Down:
	                player.tempPos.y--;
                    if (!(player.PositionCheck())) player.tempPos.y++;
                    else
                    {
                        player.soundmanager.Play(SoundManager.Sounds.Move1);
                        nextTurn = true;
                    }
                    break;
            }

            if (nextTurn) GameStateManager.Instance.NextTurn();
        }
    }

    public class BlinkCommand : Command
    {
        public Direction dir;

        public BlinkCommand(Direction dir)
        {
            this.dir = dir;
        }

        public override void Execute(Player player)
        {
            player.tempPos.x = player.pos.X; player.tempPos.y = player.pos.Y;

            bool nextTurn = false;

            switch (dir)
            {
                case Direction.Left:
                    player.TurnLeft();
                    player.tempPos.x = player.tempPos.x - 3;
                    if (!(player.PositionCheck()))
                        player.tempPos.x = player.tempPos.x + 3;
                    else
                    {
                        //player.Blinkable = player.Difficulty;
                        GameStateManager.Instance.soundManager.Play(SoundManager.Sounds.Blink);
                        nextTurn = true;
                    }
                    break;
                case Direction.Right:
                    player.TurnRight();
                    player.tempPos.x = player.tempPos.x + 3;
                    if (!(player.PositionCheck()))
                        player.tempPos.x = player.tempPos.x - 3;
                    else
                    {
                        //player.Blinkable = player.Difficulty;
                        GameStateManager.Instance.soundManager.Play(SoundManager.Sounds.Blink);
                        nextTurn = true;
                    }
                    break;
                case Direction.Up:
                    player.tempPos.y = player.tempPos.y + 3;
                    if (!(player.PositionCheck()))
                        player.tempPos.y = player.tempPos.y - 3;
                    else
                    {
                        //player.Blinkable = player.Difficulty;
                        GameStateManager.Instance.soundManager.Play(SoundManager.Sounds.Blink);
                        nextTurn = true;
                    }
                    break;
                case Direction.Down:
                    player.tempPos.y = player.tempPos.y - 3;
                    if (!(player.PositionCheck()))
                        player.tempPos.y = player.tempPos.y + 3;
                    else
                    {
                        //player.Blinkable = player.Difficulty;
                        GameStateManager.Instance.soundManager.Play(SoundManager.Sounds.Blink);
                        nextTurn = true;
                    }
                    break;
            }

            if (nextTurn) GameStateManager.Instance.NextTurn();
        }
    }
}