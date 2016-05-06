using System;
using UnityEngine;

// Base monster class. Inherit from this when creating new monsters.

[RequireComponent(typeof(Position))]
public class Monster : MonoBehaviour
{
    public int MaxHealth;
    public int DamageToPlayer;
    public int DamageToSelf;
    public TileColor monstersColor;

    public int Health;

    public Position pos;

    protected void Awake()
    {
        pos = GetComponent<Position>();
    }

    protected void Start()
    {
        Health = MaxHealth;
        GameStateManager.Instance.AddMonster(this);
        GameStateManager.Instance.MonsterTurns += OnTurn;
        GameStateManager.Instance.MonsterResets += OnReset;
    }

    // Override this!!!
    protected virtual void OnTurn()
    {
        TileData currentTileData = TileManager.Instance.GetTileType(pos.X, pos.Y);
        if (currentTileData.color == monstersColor && currentTileData.color != TileColor.None)
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
            player.RevertTurn();
        }
        pos.X = x;
        pos.Y = y;
    }
}