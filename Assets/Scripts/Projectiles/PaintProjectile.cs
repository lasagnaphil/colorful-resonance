using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class PaintProjectile : Projectile
{
    public TileColor BombColor;

    protected override void DestroyByCollision()
    {
        GameObject effectParticle = Instantiate(destroyEffectObject, transform.position + new Vector3(0,0,-5), Quaternion.identity) as GameObject;
        soundManager.Play(SoundManager.Sounds.Explosion);
        effectParticle.GetComponent<ParticleSystem>().startSize = 6;
        Destroy(effectParticle, 2);
    }

    protected override void OnTurn(Sequence sequence)
    {
        if ((TileManager.Instance.GetTileType(pos.X, pos.Y) == TileType.Wall || TileManager.Instance.GetTileType(pos.X, pos.Y) == TileType.None) && Type != ProjectileType.GoThrough)
        {
            Destroy(gameObject);
            for(int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (TileManager.Instance.GetTileType(pos.X - 1 + k, pos.Y - 1 + j) != TileType.None &&
                        GameStateManager.Instance.CheckOrbPosition(pos.X - 1 + k, pos.Y - 1 + j) == null)
                    {
                        TileManager.Instance.SetTileColor(pos.X - 1 + k, pos.Y - 1 + j, BombColor);
                    }
                }
            }
        }
    }
}
