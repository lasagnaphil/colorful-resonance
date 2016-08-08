using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

// Base monster class. Inherit from this when creating new monsters.

public class Monster : GameEntity
{
    public int MaxHealth;
    public int DamageToPlayer;
    public int DamageToSelf;
    public TileColor monstersColor;

    public int Health;
    public SoundManager soundmanager;

    public bool moveCancelled = false;
    protected bool applicationIsQuitting = false;
    public bool destroyed = false;

    protected bool CheckBeforeDestroy
    {
        get { return GameStateManager.Instance != null && !GameStateManager.Instance.IsLoading && !applicationIsQuitting; }
    }

    protected void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        Health = MaxHealth;
        GameStateManager.Instance.AddMonster(this);
        GameStateManager.Instance.MonsterTurns += OnTurnInternal;
        GameStateManager.Instance.MonsterResets += OnReset;
        soundmanager = GameStateManager.Instance.soundManager;
    }

    // Override this!!!
    protected virtual Sequence OnTurnInternal()
    {
        Sequence sequence = DOTween.Sequence();

        Tile currentTile = TileManager.Instance.GetTile(pos.X, pos.Y);
        TileData currentTileData = currentTile.Data;
        if (currentTileData.color == monstersColor && currentTileData.color != TileColor.None && currentTile.Activated)
        {
            Health -= 1;
            CheckHealth();
        }

        if (destroyed) return sequence;
        if (moveCancelled)
        {
            moveCancelled = false;
            return sequence;
        }

        OnTurn(sequence);

        return sequence;
    }

    protected virtual void OnTurn(Sequence sequence) { }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            if (CheckBeforeDestroy) GameStateManager.Instance.RemoveMonster(this);
            destroyed = true;
            GameStateManager.Instance.soundManager.Play(SoundManager.Sounds.MonsterDie);
        }
    }

    protected virtual void OnReset()
    {
        Health = MaxHealth;
    }

    protected virtual void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.MonsterTurns -= OnTurnInternal;
            GameStateManager.Instance.MonsterResets -= OnReset;
            if (CheckBeforeDestroy)
            {
                WhenDestroyed();
                if (GameStateManager.Instance.MonsterDied != null)
                    GameStateManager.Instance.MonsterDied();
            }
        }
    }

    protected virtual void WhenDestroyed() { }

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
            sequence.AppendCallback(() => player.GetComponent<Animator>().SetTrigger("Hit"));
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