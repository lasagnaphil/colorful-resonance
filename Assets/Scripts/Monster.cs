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

    public bool moveCancelled = false;
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
            if (CheckBeforeDestroy) GameStateManager.Instance.RemoveMonster(this);
        }

        return sequence;
    }

    protected virtual void OnReset()
    {
        Health = MaxHealth;
    }

    protected virtual void OnDestroy()
    {
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

    // This function does not check player position anymore...
    public void Move(int x, int y)
    {
        pos.X = x;
        pos.Y = y;
    }

    // This function returns false if the move has succeeded, and
    // returns true if the move has failed (because of player)
    public bool AnimatedMove(Sequence sequence, int x, int y)
    {
        // Check if another monster is in the position we want to move
        Monster monster = GameStateManager.Instance.CheckMonsterPosition(x, y);
        if (monster != null)
        {
            return false;
        }

        // Check if the player is in the position we want to move
        var player = GameStateManager.Instance.player;
        if (x == player.pos.X && y == player.pos.Y)
        {
            var prevPos = new Vector2i(this.pos.X, this.pos.Y);
            player.ApplyDamage(DamageToPlayer);
            sequence.Append(pos.AnimatedMove(x, y, 0.2f));
            sequence.Append(pos.AnimatedMove(prevPos.x, prevPos.y, 0.2f));
            return false;
        }
        else
        {
            sequence.Append(pos.AnimatedMove(x, y, 0.2f));
            return true;
        }
    }
}