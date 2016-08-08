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

    protected override void OnTurn(Sequence sequence)
    {
        if(RainCoolTime < 3)
            RainCoolTime++;
        else if (RainCoolTime == 3)
        {
            Width = TileManager.Instance.width;
            Height = TileManager.Instance.height;

            Vector2i spawnPoint = PlayerPos();
            SpawnProjectile(RainProjectile, spawnPoint.x, spawnPoint.y, Direction.None);
            RainCoolTime = 0;
        }
    }

    protected override void WhenDestroyed()
    {
        
        base.WhenDestroyed();
    }
}