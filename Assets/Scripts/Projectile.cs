using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public enum ProjectileType
{
    Normal, GoThrough
}
public class Projectile : GameEntity
{
    public int Duration; // if duration is -1 then never dies
    public int Damage;
    public Direction MovingDirection;
    public ProjectileType Type;
    public GameObject destroyEffectObject;

    protected Vector2i prevPos;
    public bool updateDirection = true;
    public SoundManager soundmanager;
    protected Player player;

    protected void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        player = GameStateManager.Instance.player;
        GameStateManager.Instance.AddProjectile(this);
        GameStateManager.Instance.ProjectileTurns += OnTurnInternal;
        soundmanager = GameStateManager.Instance.soundManager;
    }

    private Sequence OnTurnInternal()
    {
        Sequence sequence = DOTween.Sequence();

        // Store the previous location
        prevPos = pos.GetVector2i();

        // Move the projectile based on the position
        var moveDir = DirectionHelper.ToVector2i(MovingDirection);
        sequence.Append(pos.AnimatedMove(pos.X + moveDir.x, pos.Y + moveDir.y, 0.2f));

        // Update the projectile rotation
        if (updateDirection)
            transform.rotation = DirectionHelper.ToQuaternion(MovingDirection);

        OnTurn(sequence);

        CheckAndDestroy(sequence);

        return sequence;
    }

    protected virtual void OnTurn(Sequence sequence) { }

    protected virtual void DestroyByCollision()
    {
        if (destroyEffectObject == null) return ;
        GameObject effectParticle = Instantiate(destroyEffectObject, transform.position + new Vector3(0,0,-1), Quaternion.identity) as GameObject;
        effectParticle.GetComponent<ParticleSystem>().startSize = 3;
        Destroy(effectParticle, 2);
    }

    private void CheckAndDestroy(Sequence sequence)
    {
        // If the position of the projectile is in the player's location
        // then apply damage to player
        if ((player.pos.X == pos.X && player.pos.Y == pos.Y) ||
            (player.pos.X == prevPos.x && player.pos.Y == prevPos.y))
        {
            player.ApplyDamage(Damage);
            if (Type != ProjectileType.GoThrough)
            {
                DestroyByCollision();
                Destroy(this.gameObject);
                return;
            }
        }

        if (TileManager.Instance.GetTileType(pos.X, pos.Y) == TileType.Wall &&
            Type != ProjectileType.GoThrough)
        {
            DestroyByCollision();
            Destroy(this.gameObject);
            return;
        }

        // If the projectile is out of bounds, then destroy it
        if (pos.X < 0 || pos.Y < 0 ||
            pos.X >= TileManager.Instance.width || pos.Y >= TileManager.Instance.height)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.RemoveProjectile(this);
            GameStateManager.Instance.ProjectileTurns -= OnTurnInternal;
        }
    }
}
