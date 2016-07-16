using System.Collections;

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
            switch (dir)
            {
                case Direction.Left:
	                player.TurnLeft();
	                player.tempPos.x--;
	                if (!(player.PositionCheck())) player.tempPos.x++;
	                else player.soundManager.Play(SoundManager.Sounds.Move1);
                    break;
                case Direction.Right:
	                player.TurnRight();
                    player.tempPos.x++;
                    if (!(player.PositionCheck())) player.tempPos.x--;
	                else player.soundManager.Play(SoundManager.Sounds.Move1);
                    break;
                case Direction.Up:
                    player.tempPos.y++;
	                if (!(player.PositionCheck())) player.tempPos.y--;
	                else player.soundManager.Play(SoundManager.Sounds.Move1);
                    break;
                case Direction.Down:
	                player.tempPos.y--;
	                if (!(player.PositionCheck())) player.tempPos.y++;
	                else player.soundManager.Play(SoundManager.Sounds.Move1);
                    break;
            }
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
            switch (dir)
            {
                case Direction.Left:
                    player.TurnLeft();
                    player.tempPos.x = player.tempPos.x - 3;
                    if (!(player.PositionCheck()))
                        player.tempPos.x = player.tempPos.x + 3;
                    else
                    {
                        player.Blinkable = player.Difficulty;
                        player.soundManager.Play(SoundManager.Sounds.Blink);
                    }
                    break;
                case Direction.Right:
                    player.TurnRight();
                    player.tempPos.x = player.tempPos.x + 3;
                    if (!(player.PositionCheck()))
                        player.tempPos.x = player.tempPos.x - 3;
                    else
                    {
                        player.Blinkable = player.Difficulty;
                        player.soundManager.Play(SoundManager.Sounds.Blink);
                    }
                    break;
                case Direction.Up:
                    player.tempPos.y = player.tempPos.y + 3;
                    if (!(player.PositionCheck()))
                        player.tempPos.y = player.tempPos.y - 3;
                    else
                    {
                        player.Blinkable = player.Difficulty;
                        player.soundManager.Play(SoundManager.Sounds.Blink);
                    }
                    break;
                case Direction.Down:
                    player.tempPos.y = player.tempPos.y - 3;
                    if (!(player.PositionCheck()))
                        player.tempPos.y = player.tempPos.y + 3;
                    else
                    {
                        player.Blinkable = player.Difficulty;
                        player.soundManager.Play(SoundManager.Sounds.Blink);
                    }
                    break;
            }
    }
}