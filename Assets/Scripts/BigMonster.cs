using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BigMonster : Monster
{
    private List<Vector2i> collider = new List<Vector2i>();
    private List<Vector2i> currentArea = new List<Vector2i>();

    public void SetCollider(List<Vector2i> collider)
    {
        this.collider = collider;
    }

    public bool Contains(Vector2i position)
    {
        return currentArea.Contains(position);
    }

    public bool Contains(IEnumerable<Vector2i> positions)
    {
        return positions.All(Contains);
    }

    public override void Move(int x, int y)
    {
        pos.X = x;
        pos.Y = y;
        currentArea = collider.Select(vec => vec + pos.GetVector2i()).ToList();
    }

    public override bool AnimatedMove(Sequence sequence, int x, int y)
    {
        // Check if another monster is in the position we want to move
        List<Monster> foundMonsters = currentArea
            .Select(vec => GameStateManager.Instance.CheckMonsterPosition(vec.x, vec.y))
            .Where(m => m != null).ToList();

        if (foundMonsters.Count > 0) return false;

        if (Contains(Player.pos.GetVector2i()))
        {
            var prevPos = pos.GetVector2i();
            Player.ApplyDamage(DamageToPlayer);
            sequence.AppendCallback(() => Player.GetComponent<Animator>().SetTrigger("Hit"));
            sequence.Append(pos.AnimatedMove(x, y, 0.2f));
            sequence.Append(pos.AnimatedMove(prevPos.x, prevPos.y, 0.2f));
            Move(x, y);
            return false;
        }
        else
        {
            sequence.Append(pos.AnimatedMove(x, y, 0.2f));
            return true;
        }
    }

    public override bool TryMove(Sequence sequence, int x, int y)
    {
        List<Vector2i> futureArea = collider.Select(vec => vec + new Vector2i(x, y)).ToList();
        bool canMove = futureArea.All(CheckTileIsNormal);
        if (canMove) AnimatedMove(sequence, x, y);
        return canMove;
    }

    public override bool TryMove(Sequence sequence, Vector2i vec)
    {
        return TryMove(sequence, vec.x, vec.y);
    }
}