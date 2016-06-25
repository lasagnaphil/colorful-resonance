using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class WaterBalloonProjectile : Projectile
{
    public TileColor BombColor;
    public GameObject effectObject;

    private int turn = 0;

    protected virtual void Start()
    {
        base.Start();
        updateDirection = false;
    }

    protected override void OnTurn(Sequence sequence)
    {
        if (GameStateManager.Instance.CheckOrbPosition(pos.X, pos.Y) == null)
            TileManager.Instance.SetTileColor(pos.X, pos.Y, BombColor);

        // if (Mathf.Abs(deltaX) == 0 && Mathf.Abs(deltaY) == 0)
        // {
        //     player.ApplyDamage(Damage);
        // }

        GameObject effectParticle = Instantiate(effectObject, transform.position + new Vector3(0,0,-5), Quaternion.identity) as GameObject;
        effectParticle.GetComponent<ParticleSystem>().startSize = 2;
        Destroy(effectParticle, 2);

        Destroy(gameObject);
    }
}
