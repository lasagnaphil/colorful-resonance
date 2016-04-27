using System;
using FullInspector;
using UnityEngine;

// Base monster class. Inherit from this when creating new monsters.

[RequireComponent(typeof(Position))]
public class Monster : BaseBehavior
{
    public int MaxHealth { get; set; }
    public int DamageToPlayer { get; set; }
    public int DamageToSelf { get; set; }
    public TileType immuneColor;
    
    [ShowInInspector]
    private int Health { get; set; }

    protected Position pos;

    protected void Start()
    {
        pos = GetComponent<Position>();
        GameStateManager.Instance.AddMonster(this);
        GameStateManager.Instance.MonsterTurns += OnTurn;
        GameStateManager.Instance.MonsterResets += OnReset;
    }

    // Override this!!!
    protected virtual void OnTurn()
    {
        TileType currentTileType = TileManager.Instance.GetTileType(pos.X, pos.Y);
        if (currentTileType != immuneColor && currentTileType != TileType.None)
        {
            DamageToPlayer -= 1;
        }
        if (DamageToPlayer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnReset()
    {
        Health = MaxHealth;
    }

    protected void OnDestroy()
    {
        // Right now null reference error
        // GameStateManager.Instance.RemoveMonster(this);
        GameStateManager.Instance.MonsterTurns -= OnTurn;
        GameStateManager.Instance.MonsterTurns -= OnReset;
    }
}