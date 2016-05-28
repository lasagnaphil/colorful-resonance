using UnityEngine;
using System.Collections;
using DG.Tweening;
using Utils;

public enum ProjectileType
{
    Normal, GoThrough
}
[RequireComponent(typeof(Position))]
public class Projectile : MonoBehaviour
{
    public int Duration; // if duration is -1 then never dies
    public int Damage;
    public Direction MovingDirection;
    public ProjectileType Type;

    public Position pos;
    protected Vector2i prevPos;

    protected Player player;

    protected void Start()
    {
        pos = GetComponent<Position>();
        player = GameStateManager.Instance.player;
        GameStateManager.Instance.AddProjectile(this);
        GameStateManager.Instance.ProjectileTurns += OnTurn;
    }

    protected virtual Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();

        // Store the previous location
        prevPos = pos.GetVector2i();

        // Move the projectile based on the position
        var moveDir = DirectionHelper.ToVector2i(MovingDirection);
        sequence.Append(pos.AnimatedMove(pos.X + moveDir.x, pos.Y + moveDir.y, 0.2f));

        return CheckAndDestroy(sequence);
    }

    protected virtual Sequence CheckAndDestroy(Sequence sequence)
    {
        // If the position of the projectile is in the player's location
        // then apply damage to player
        if ((player.pos.X == pos.X && player.pos.Y == pos.Y) ||
            (player.pos.X == prevPos.x && player.pos.Y == prevPos.y &&
             player.prevPos.x == pos.X && player.prevPos.y == pos.Y))
        {
            player.ApplyDamage(Damage);
            if (Type != ProjectileType.GoThrough)
            {
                Destroy(this.gameObject);
                return sequence;
            }
        }

        if (TileManager.Instance.GetTileType(pos.X, pos.Y) == TileType.Wall &&
            Type != ProjectileType.GoThrough)
        {
            Destroy(this.gameObject);
            return sequence;
        }

        // If the projectile is out of bounds, then destroy it
        if (pos.X < 0 || pos.Y < 0 ||
            pos.X >= TileManager.Instance.width || pos.Y >= TileManager.Instance.height)
        {
            Destroy(this.gameObject);
        }

        return sequence;
        
    }

    protected void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.RemoveProjectile(this);
            GameStateManager.Instance.ProjectileTurns -= OnTurn;
        }
    }
}
