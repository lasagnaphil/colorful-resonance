using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class RainProjectile : Projectile
{
    public TileColor BombColor;
    public GameObject effectObject;

    private int turn = 0;

    protected override void OnTurn(Sequence sequence)
    {
        for (int k = 0; k < 3; k++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (TileManager.Instance.GetTileType(pos.X - 1 + k, pos.Y - 1 + j) != TileType.Wall &&
                    TileManager.Instance.GetTileType(pos.X - 1 + k, pos.Y - 1 + j) != TileType.None &&
                    GameStateManager.Instance.CheckOrbPosition(pos.X - 1 + k, pos.Y - 1 + j) == null)
                {
                    TileManager.Instance.SetTileColor(pos.X - 1 + k, pos.Y - 1 + j, BombColor);
                    Destroy(Instantiate(effectObject, TileManager.Instance.GetTile(pos.X - 1 + k, pos.Y - 1 + j).transform.position + new Vector3(0,0,-1), Quaternion.identity) as GameObject, 2);
                }
            }
        }

        Destroy(gameObject);
    }
}