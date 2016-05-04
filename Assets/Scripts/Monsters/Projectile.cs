using UnityEngine;
using System.Collections;
using Utils;

[RequireComponent(typeof(Position))]
public class Projectile : MonoBehaviour
{
    public int Duration; // if duration is -1 then never dies
    public int Damage;
    public Direction MovingDirection;

    protected Position pos;
    protected Vector2i prevPos;

    protected Player player;

    protected void Start()
    {
        pos = GetComponent<Position>();
        player = GameStateManager.Instance.player;
        GameStateManager.Instance.ProjectileTurns += OnTurn;
    }

    protected virtual void OnTurn()
    {
        // Store the previous location
        prevPos = pos.GetVector2i();

        // Move the projectile based on the position
        pos.Add(DirectionHelper.ToVector2i(MovingDirection));

        // If the position of the projectile is in the player's location
        // then apply damage to player
        if ((player.pos.X == pos.X && player.pos.Y == pos.Y) ||
            (player.pos.X == prevPos.x && player.pos.Y == prevPos.y &&
             player.prevPos.x == pos.X && player.prevPos.y == pos.Y))
        {
            player.ApplyDamage(Damage);
        }

        // If the projectile is out of bounds, then destroy it
        if (pos.X < 0 || pos.Y < 0 ||
            pos.X >= TileManager.Instance.width || pos.Y >= TileManager.Instance.height)
        {
            Destroy(this.gameObject);
        }
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
