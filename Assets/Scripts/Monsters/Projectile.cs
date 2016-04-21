using UnityEngine;
using System.Collections;
using FullInspector;
using Utils;

[RequireComponent(typeof(Position))]
public class Projectile : BaseBehavior
{
    public int Duration { get; set; } // if duration is -1 then never dies
    public int Damage { get; set; }
    public Direction MovingDirection { get; set; }

    protected Position pos;
    protected Player player;

    protected void Start()
    {
        pos = GetComponent<Position>();
        player = GameStateManager.Instance.player;
        GameStateManager.Instance.ProjectileTurns += OnTurn;
    }

    protected virtual void OnTurn()
    {
        // Move the projectile based on the position
        pos.Add(DirectionHelper.ToVector2i(MovingDirection));

        // If the position of the projectile is in the player's location
        // then apply damage to player
        if (player.pos.X == pos.X && player.pos.Y == pos.Y)
        {
            player.ApplyDamage(Damage);
        }
        
    }
}
