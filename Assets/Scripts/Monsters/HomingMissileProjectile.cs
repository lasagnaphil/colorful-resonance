using UnityEngine;
using System.Collections;
using Utils;
using DG.Tweening;

public class HomingMissileProjectile : Projectile
{
    public GameObject destroyEffectObject;
    
    int deltaX;
    int deltaY;
    
    protected override void OnDestroy()
    {
        GameObject effectParticle = Instantiate(destroyEffectObject, transform.position + new Vector3(0,0,-1), Quaternion.identity) as GameObject;
        effectParticle.GetComponent<ParticleSystem>().startSize = 3;
        Destroy(effectParticle, 2);
        
        base.OnDestroy();
    }
    
    protected override Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();
        base.OnTurn();
        deltaX = player.pos.X - pos.X;
        deltaY = player.pos.Y - pos.Y;

        if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
        {
            if (deltaX > 0)
            {
                MovingDirection = Direction.Right;
            }
            else if (deltaX < 0)
            {
                MovingDirection = Direction.Left;
            }
        }
        else
        {
            if (deltaY > 0)
            {
                MovingDirection = Direction.Up;
            }
            else if (deltaY < 0)
            {
                MovingDirection = Direction.Down;
            }
        }

        return sequence;
    }
}
