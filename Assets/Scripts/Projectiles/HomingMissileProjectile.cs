using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class HomingMissileProjectile : Projectile
{
    // protected override void DestroyByCollision()
    // {
    //     GameObject effectParticle = Instantiate(destroyEffectObject, transform.position + new Vector3(0,0,-1), Quaternion.identity) as GameObject;
    //     effectParticle.GetComponent<ParticleSystem>().startSize = 3;
    //     Destroy(effectParticle, 2);
    // }
    
    protected override void OnTurn(Sequence sequence)
    {
        Vector2i delta = DiffFromPlayer();
        if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                MovingDirection = Direction.Right;
            }
            else if (delta.x < 0)
            {
                MovingDirection = Direction.Left;
            }
        }
        else
        {
            if (delta.y > 0)
            {
                MovingDirection = Direction.Up;
            }
            else if (delta.y < 0)
            {
                MovingDirection = Direction.Down;
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
