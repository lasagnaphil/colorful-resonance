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
    public TileColor immuneColor;
    
    [ShowInInspector]
    private int Health { get; set; }

    protected Position pos;

    protected void Start()
    {
        Health = MaxHealth;
        pos = GetComponent<Position>();
        GameStateManager.Instance.AddMonster(this);
        GameStateManager.Instance.MonsterTurns += OnTurn;
        GameStateManager.Instance.MonsterResets += OnReset;
    }

    // Override this!!!
    protected virtual void OnTurn()
    {
        TileData currentTileData = TileManager.Instance.GetTileType(pos.X, pos.Y);
        if (currentTileData.color != immuneColor && currentTileData.color != TileColor.None)
        {
            Health -= 1;
        }
        if (Health <= 0)
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
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.RemoveMonster(this);
            GameStateManager.Instance.MonsterTurns -= OnTurn;
            GameStateManager.Instance.MonsterTurns -= OnReset;
        }
    }

    public void Move(int x, int y)
    {
        var player = GameStateManager.Instance.player;
        if (x == player.pos.X && y == player.pos.Y)
        {
            player.ApplyDamage(DamageToPlayer);
        }
        else
        {
            pos.X = x;
            pos.Y = y;
        }
    }
}