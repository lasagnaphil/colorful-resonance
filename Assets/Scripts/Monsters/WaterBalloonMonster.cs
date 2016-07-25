using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class WaterBalloonMonster : Monster
{
    public Projectile WaterBalloonProjectile;

    int BalloonCoolTime;

    protected override void OnTurn(Sequence sequenec)
    {
        if(BalloonCoolTime < 3)
            BalloonCoolTime++;
        else if (BalloonCoolTime == 3)
        {
            Vector2i spawnPoint = PlayerPos();

            SpawnProjectile(WaterBalloonProjectile, spawnPoint.x, spawnPoint.y, Direction.None);
            BalloonCoolTime = 0;
        }
    }

    protected override void WhenDestroyed()
    {
        base.WhenDestroyed();
    }
}