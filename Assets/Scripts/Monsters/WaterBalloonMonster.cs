using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public class WaterBalloonMonster : Monster
{
    public Projectile WaterBalloonProjectile;

    int BalloonCoolTime;
    int SpawnPointX;
    int SpawnPointY;

    protected override Sequence OnTurn()
    {
        Sequence sequence = base.OnTurn();
        
        if(BalloonCoolTime < 3)
            BalloonCoolTime++;
        else if (BalloonCoolTime == 3)
        {
            SpawnPointX = GameStateManager.Instance.player.GetComponent<Position>().X;
            SpawnPointY = GameStateManager.Instance.player.GetComponent<Position>().Y;

            Direction moveDir = Direction.None;
            GameStateManager.Instance.SpawnProjectile(WaterBalloonProjectile, SpawnPointX, SpawnPointY, moveDir);
            BalloonCoolTime = 0;
        }

        return sequence;
    }
}