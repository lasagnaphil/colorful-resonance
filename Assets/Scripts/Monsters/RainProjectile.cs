using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class RainProjectile : Projectile
{
    public GameObject effectObject;
    
    protected override Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();
        base.OnTurn();
        int n = 0;

        if (n == 1)
        {
            Destroy(gameObject);
            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (TileManager.Instance.GetTileType(pos.X - 1 + k, pos.Y - 1 + j) != TileType.Wall && TileManager.Instance.GetTileType(pos.X - 1 + k, pos.Y - 1 + j) != TileType.None)
                    {
                        TileManager.Instance.SetTileColor(pos.X - 1 + k, pos.Y - 1 + j, TileColor.White);
                    }
                }
            }
        }     

        n++;
        return CheckAndDestroy(sequence);
    }
}