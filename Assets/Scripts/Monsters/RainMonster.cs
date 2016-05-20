using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class RainMonster : Monster
{
    public Projectile RainProjectile;

    int RainCoolTime;
    int Width;
    int Height;
    int SpawnPointX;
    int SpawnPointY;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();
        
        if(RainCoolTime < 3)
            RainCoolTime++;
        else if (RainCoolTime == 3)
        {
            Width = TileManager.Instance.width;
            Height = TileManager.Instance.height;

            SpawnPointX = Random.Range(1, Width - 1);
            SpawnPointY = Random.Range(1, Height - 1);

            Direction moveDir = Direction.None;
            GameStateManager.Instance.SpawnProjectile(RainProjectile, SpawnPointX, SpawnPointY, moveDir);
            RainCoolTime = 0;
        }

        return sequence;
    }
}