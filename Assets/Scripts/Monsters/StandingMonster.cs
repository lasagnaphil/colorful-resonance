using DG.Tweening;

namespace Monsters
{
    public class StandingMonster : Monster
    {
        protected override Sequence OnTurn()
        {
            Sequence sequence = base.OnTurn();
            return sequence;
        }
    }
}