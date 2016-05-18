using System;
using DG.Tweening;
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

    protected bool applicationIsQuitting = false;

    protected bool CheckBeforeDestroy
    {
        get { return GameStateManager.Instance != null && !GameStateManager.Instance.IsLoading && !applicationIsQuitting; }
    }

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
    protected virtual Sequence OnTurn()
    {
        Sequence sequence = DOTween.Sequence();

        Tile currentTile = TileManager.Instance.GetTile(pos.X, pos.Y);
        TileData currentTileData = currentTile.Data;
        if (currentTileData.color == monstersColor && currentTileData.color != TileColor.None && currentTile.Activated)
        {
            Health -= 1;
        }
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }

        return sequence;
    }

    protected virtual void OnReset()
    {
        Health = MaxHealth;
    }

    protected virtual void OnDestroy()
    {
        if (CheckBeforeDestroy)
        {
            GameStateManager.Instance.RemoveMonster(this);
        }
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.MonsterTurns -= OnTurn;
            GameStateManager.Instance.MonsterResets -= OnReset;
        }
    }

    protected void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }

    public void CheckForPlayer(int x, int y)
    {
        var player = GameStateManager.Instance.player;
        if (x == player.pos.X && y == player.pos.Y)
        {
            player.ApplyDamage(DamageToPlayer);
            player.RevertTurn();
        }
    }
    public void Move(int x, int y)
    {
        CheckForPlayer(x, y);
        pos.X = x;
        pos.Y = y;
    }

    public Tween SequencedMove(int x, int y)
    {
        CheckForPlayer(x, y);
        return pos.AnimatedMove(x, y, 0.2f);
    }
}