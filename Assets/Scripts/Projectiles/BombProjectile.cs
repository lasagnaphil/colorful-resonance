using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class BombProjectile : Projectile
{
    public TileColor BombColor;
    public GameObject effectObject;

    private int turn = 0;

    protected override void Start()
    {
        base.Start();
        updateDirection = false;
    }

    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();
        for (int k = 0; k < 3; k++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (TileManager.Instance.GetTileType(pos.X - 1 + k, pos.Y - 1 + j) != TileType.Wall && TileManager.Instance.GetTileType(pos.X - 1 + k, pos.Y - 1 + j) != TileType.None)
                {
                    TileManager.Instance.SetTileColor(pos.X - 1 + k, pos.Y - 1 + j, BombColor);
                }
            }
        }

        if (Mathf.Abs(delta.x) <= 1 && Mathf.Abs(delta.y) <= 1)
        {
            player.ApplyDamage(Damage);
        }

        GameObject effectParticle = Instantiate(effectObject, transform.position + new Vector3(0,0,-5), Quaternion.identity) as GameObject;
        effectParticle.GetComponent<ParticleSystem>().startSize = 6;
        Destroy(effectParticle, 2);

        Destroy(gameObject);
    }
}
