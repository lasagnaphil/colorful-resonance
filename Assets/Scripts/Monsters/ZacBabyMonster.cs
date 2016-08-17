using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Utils;

public class ZacBabyMonster : Monster
{
    public Direction firstMoveDir = Direction.None;
	private int CoolTime;
    private bool firstTurn = true;

    public override void Setup(object[] args)
    {
        if (args.Length > 1)
        {
            Debug.LogError("Parameter mismatch in ZacBabyMonster!");
            return;
        }
        if (args.Length == 1)
        {
            Direction? dir = args[0] as Direction?;
            if (dir.HasValue) firstMoveDir = dir.Value;
        }
    }
	protected override void OnTurn(Sequence sequence)
	{
	    if (firstTurn)
	    {
	        Vector2i movePos = pos.GetVector2i() + firstMoveDir.ToVector2i();
	        TryMove(sequence, movePos.x, movePos.y);
	        firstTurn = false;
	    }
		Vector2i delta = DiffFromPlayer();

		if (CoolTime < 4)
			CoolTime++;
		else if(CoolTime == 4)
		{
			if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
			{
				if (delta.x > 0)
				{
					if (CheckTileIsNormal(pos.X + 1, pos.Y))
					{
						AnimatedMove(sequence, pos.X + 1, pos.Y);
					}
				}
				else if (delta.x < 0)
				{
					if (CheckTileIsNormal(pos.X - 1, pos.Y))
					{
						AnimatedMove(sequence, pos.X - 1, pos.Y);
					}
				}
			}
			else
			{
				if (delta.y > 0)
				{
					if (CheckTileIsNormal(pos.X, pos.Y + 1))
					{
						AnimatedMove(sequence, pos.X, pos.Y + 1);
					}
				}
				else if (delta.y < 0)
				{
					if (CheckTileIsNormal(pos.X, pos.Y - 1))
					{
						AnimatedMove(sequence, pos.X, pos.Y - 1);
					}
				}
			}
			CoolTime = 0;
		}
	}
}

