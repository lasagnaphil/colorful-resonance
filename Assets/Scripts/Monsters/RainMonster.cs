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

            SpawnPointX = GameStateManager.Instance.player.GetComponent<Position>().X;
            SpawnPointY = GameStateManager.Instance.player.GetComponent<Position>().Y;

            SpawnProjectile(RainProjectile, SpawnPointX, SpawnPointY, Direction.None);
            RainCoolTime = 0;
        }
    }
}