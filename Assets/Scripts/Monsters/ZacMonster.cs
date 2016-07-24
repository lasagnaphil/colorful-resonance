using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class ZacMonster : Monster
{
    int CoolTime;

    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();

        if (CoolTime < 3)
            CoolTime++;
        else if(CoolTime == 3)
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
    protected override void WhenDestroyed()
    {
        base.WhenDestroyed();
		if (CheckTileIsNormal (pos.X + 2, pos.Y) && CheckTileIsNormal (pos.X - 2, pos.Y))
		{
			SpawnMonster (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster ("ZacBaby"), pos.X + 2, pos.Y);
			SpawnMonster (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster ("ZacBaby"), pos.X - 2, pos.Y);
		}
		else if (CheckTileIsNormal (pos.X, pos.Y - 2) && CheckTileIsNormal (pos.X, pos.Y + 2))
		{
			SpawnMonster (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster ("ZacBaby"), pos.X, pos.Y - 2);
			SpawnMonster (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster ("ZacBaby"), pos.X, pos.Y + 2);
		}
		else if (CheckTileIsNormal (pos.X + 2, pos.Y) && CheckTileIsNormal (pos.X, pos.Y + 2))
		{
			SpawnMonster (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster ("ZacBaby"), pos.X + 2, pos.Y);
			SpawnMonster (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster ("ZacBaby"), pos.X, pos.Y + 2);
		}
		else if (CheckTileIsNormal (pos.X - 2, pos.Y) && CheckTileIsNormal (pos.X, pos.Y - 2))
		{
			SpawnMonster (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster ("ZacBaby"), pos.X - 2, pos.Y);
			SpawnMonster (PrefabDictionary.Instance.monsterPrefabDictionary.GetMonster ("ZacBaby"), pos.X, pos.Y - 2);
		}
    }
}

