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

            if (moveCount == moveCooldown)
            {
                if (isFacingUp) sequence.Append(pos.AnimatedMove(pos.X, pos.Y + 1, 0.2f));
                else sequence.Append(pos.AnimatedMove(pos.X, pos.Y - 1, 0.2f));
                isFacingUp = !isFacingUp;
                
                moveCount = 0;    
            }
            moveCount++;

            return sequence;
        }
    }
}