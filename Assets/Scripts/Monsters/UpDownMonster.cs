using DG.Tweening;

namespace Monsters
{
    public class UpDownMonster : Monster
    {
        public bool isFacingUp = false;
        public int moveCooldown = 2;
        int moveCount = 1;
        
        protected override Sequence OnTurn()
        {
            Sequence sequence = base.OnTurn();

            if (moveCancelled)
            {
                moveCancelled = false;
                return sequence;
            }

            if (moveCount == moveCooldown)
            {
                var newPos = isFacingUp ? new Vector2i(pos.X, pos.Y + 1) : new Vector2i(pos.X, pos.Y - 1);
                // if the move is successful then reset the counter
                if (AnimatedMove(sequence, newPos.x, newPos.y))
                {
                    isFacingUp = !isFacingUp;
                    moveCount = 0;
                }
                // else... make it do the thing next turn
                else
                {
                    moveCount -= 1;
                }
            }
            moveCount++;

            return sequence;
        }
    }
}